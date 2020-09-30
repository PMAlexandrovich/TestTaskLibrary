using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Library;

namespace TestTaskLibrary.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IBooksRepository booksRepository;
        private readonly LibraryManager libraryManager;
        private readonly UserManager<User> userManager;
        private readonly BookReviewsManager commentManager;
        private readonly IMediator mediator;

        public LibraryController(IBooksRepository booksRepository, LibraryManager libraryManager, UserManager<User> userManager, BookReviewsManager commentManager, IMediator mediator)
        {
            this.booksRepository = booksRepository;
            this.libraryManager = libraryManager;
            this.userManager = userManager;
            this.commentManager = commentManager;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
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
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int id, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                libraryManager.Book(user, id);
            }
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(int id, string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            libraryManager.Unbook(id);
            return RedirectToAction("List", new { search, fieldSearch });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> BookDatails(GetBookByIdQuery query)
        {
            var book = await mediator.Send(query);

            if (book != null)
            {
                return View(book);
            }

            return NotFound();
        }

        public IActionResult SendReview(int userId, int bookId, int rating, string content)
        {
            mediator.Send( new CreateReviewCommand(userId, bookId, rating, content));
            return RedirectToAction("BookDatails", new { id = bookId });
        }
    }
}
