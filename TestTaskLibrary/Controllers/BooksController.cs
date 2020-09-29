using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands;
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
        private readonly IMediator mediator;
        IBooksRepository booksRepository;
        UserManager<User> userManager;
        LibraryManager libraryManager;

        public BooksController(IMediator mediator, IBooksRepository booksRepository, UserManager<User> userManager, LibraryManager libraryManager)
        {
            this.mediator = mediator;
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
            return View(booksRepository.GetAll
                .Include(b=> b.CurrentBookStatus)
                .ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new CreateBookCommand());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBookCommand command)
        {
            if (ModelState.IsValid)
            {
                int id = await mediator.Send(command);
                return RedirectToAction("List");
            }
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteBookCommand command)
        {
            int id = await mediator.Send(command);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Issue(int id)
        {
            Book book = booksRepository.GetAll.Include(b=>b.CurrentBookStatus).ThenInclude(s=> s.User).FirstOrDefault(b=>b.Id == id);
            if (book != null)
            {
                return View(new IssueViewModel() { BookId = book.Id, UserEmail = book.CurrentBookStatus.User?.Email});
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