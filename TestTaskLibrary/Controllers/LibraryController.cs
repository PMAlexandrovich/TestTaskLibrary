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

        public async Task<IActionResult> List(string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            var books = await mediator.Send(new GetBooksQuery());

            if (search != null)
            {
                switch (fieldSearch)
                {
                    case FieldSearchType.Author:
                        books = books.Where(b => b.Author.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case FieldSearchType.Title:
                        books = books.Where(b => b.Title.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case FieldSearchType.Genre:
                        books = books.Where(b => b.Genre.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    default:
                        books = books.Where(b => b.Title.ToLower().Contains(search.ToLower())).ToList();
                        break;
                }
            }
            var bookViewModel = new LibraryListViewModel() { Search = search, SearchType = fieldSearch, Books = books };
            ViewBag.User = await userManager.GetUserAsync(User);
            return View(bookViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Book(int id, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                await libraryManager.BookAsync(user, id);
            }
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unbook(int id, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            await libraryManager.UnbookAsync(id);
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
