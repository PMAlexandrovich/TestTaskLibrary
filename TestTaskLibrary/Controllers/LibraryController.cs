﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Library;

namespace TestTaskLibrary.Controllers
{
    public class LibraryController : Controller
    {
        IBooksRepository booksRepository;
        LibraryManager libraryManager;
        UserManager<User> userManager;

        public LibraryController(IBooksRepository booksRepository, LibraryManager libraryManager, UserManager<User> userManager)
        {
            this.booksRepository = booksRepository;
            this.libraryManager = libraryManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public async Task<IActionResult> List(string search = null, FieldSearchType fieldSearch = FieldSearchType.Title)
        {
            IEnumerable<Book> books;
            var booksRequest = booksRepository.Books.Include(b => b.BookStatus).ThenInclude(b => b.User);
            if (search == null)
                books = booksRequest.AsEnumerable();
            else
            {
                switch (fieldSearch)
                {
                    case FieldSearchType.Author:
                        books = booksRequest.Where(b => b.Author.Contains(search)).AsEnumerable();
                        break;
                    case FieldSearchType.Title:
                        books = booksRequest.Where(b => b.Title.Contains(search)).AsEnumerable();
                        break;
                    case FieldSearchType.Genre:
                        books = booksRequest.Where(b => b.Genre.Contains(search)).AsEnumerable();
                        break;
                    default:
                        books = booksRequest.Where(b => b.Title.Contains(search)).AsEnumerable();
                        break;
                }
            }
            var viewBooks = books.Select(b => new BookItemViewModel() { Author = b.Author, Genre = b.Genre, Id = b.Id, Status = b.BookStatus.Status, Title = b.Title, User = b.BookStatus.User }).AsEnumerable();
            var viewModel = new LibraryListViewModel() { Books = viewBooks, Search = search, SearchType = fieldSearch};
            ViewBag.User = await userManager.GetUserAsync(User);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                libraryManager.Book(user, id);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(int id)
        {
            libraryManager.Unbook(id);
            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
