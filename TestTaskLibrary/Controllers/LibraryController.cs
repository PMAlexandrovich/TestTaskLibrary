using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Application.Features.Library.Commands;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Library;
using TestTaskLibrary.Models.Reviews;

namespace TestTaskLibrary.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly IBooksRepository booksRepository;
        private readonly ILibraryManager libraryManager;
        private readonly UserManager<User> userManager;
        private readonly IBookReviewsManager commentManager;
        private readonly IMediator mediator;

        public LibraryController(IBooksRepository booksRepository, ILibraryManager libraryManager, UserManager<User> userManager, IBookReviewsManager commentManager, IMediator mediator)
        {
            this.booksRepository = booksRepository;
            this.libraryManager = libraryManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.IsInRole(RoleTypes.Admin)){
                return RedirectToAction("List", "Users");
            }
            if(User.IsInRole(RoleTypes.Librarian)){
                return RedirectToAction("List", "Books");
            }
            return RedirectToAction("List");
        }

        [AllowAnonymous]
        public async Task<IActionResult> List(string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            var books = await mediator.Send(new GetBooksQuery());

            if (search != null)
            {
                switch (fieldSearch)
                {
                    case FieldSearchType.Author:
                        books = books.Where(b => b.Author?.ToLower().Contains(search.ToLower()) ?? false).ToList();
                        break;
                    case FieldSearchType.Title:
                        books = books.Where(b => b.Title?.ToLower().Contains(search.ToLower()) ?? false).ToList();
                        break;
                    case FieldSearchType.Genre:
                        books = books.Where(b => b.Genre?.ToLower().Contains(search.ToLower()) ?? false).ToList();
                        break;
                    default:
                        books = books.Where(b => b.Title?.ToLower().Contains(search.ToLower()) ?? false).ToList();
                        break;
                }
            }
            var bookViewModel = new LibraryListViewModel() { Search = search, SearchType = fieldSearch, Books = books };
            ViewBag.User = await userManager.GetUserAsync(User);
            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookCommand command, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            await mediator.Send(command);
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(UnbookCommand command, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            await mediator.Send(command);
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [HttpGet]
        public async Task<IActionResult> Issue(GetBookByIdQuery query)
        {
            if (ModelState.IsValid)
            {
                var book = await mediator.Send(query);
                if (book != null)
                {
                    return View(new IssueBookCommand() { BookId = book.Id, UserEmail = book.CurrentStatus.User?.UserName });
                }
            }
            return RedirectToAction("List", "Books");
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Issue(IssueBookCommand command)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(command.UserEmail);
                if (user != null)
                {
                    if (await mediator.Send(command))
                    {
                        return RedirectToAction("List", "Books");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "Пользователь с таким Email не найден");
                }
            }
            return View(command);
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Take(TakeBookCommand command)
        {
            await mediator.Send(command);
            return RedirectToAction("List","Books");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
