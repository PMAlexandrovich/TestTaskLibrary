using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Infrastructure.Data;
using TestTaskLibrary.Models.Books;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = "Librarian")]
    public class BooksController : Controller
    {
        IBooksRepository booksRepository;
        UserManager<User> userManager;
        LibraryManager libraryManager;

        public BooksController(IBooksRepository booksRepository, UserManager<User> userManager, LibraryManager libraryManager)
        {
            this.booksRepository = booksRepository;
            this.userManager = userManager;
            this.libraryManager = libraryManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(booksRepository.Books
                .Include(b=> b.BookStatus)
                .ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddBookViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book() { Author = model.Author, Genre = model.Genre, Title = model.Title };
                booksRepository.Create(book);
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            booksRepository.Delete(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Issue(int id)
        {
            Book book = booksRepository.Books.Include(b=>b.BookStatus).ThenInclude(s=> s.User).FirstOrDefault(b=>b.Id == id);
            if (book != null)
            {
                return View(new IssueViewModel() { BookId = book.Id, UserEmail = book.BookStatus.User?.Email});
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Issue(IssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.UserEmail);
                if (user != null)
                {
                    libraryManager.IssueBook(user, model.BookId);
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("", "Пользователя с таким Email не существует.");
            }
            return View(model);
        }

        public IActionResult Take(int id)
        {
            libraryManager.Take(id);
            return RedirectToAction("List");
        }
    }
}