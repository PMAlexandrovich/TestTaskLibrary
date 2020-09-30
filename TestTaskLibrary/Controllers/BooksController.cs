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
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
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
        ILibraryManager libraryManager;

        public BooksController(IMediator mediator, IBooksRepository booksRepository, UserManager<User> userManager, ILibraryManager libraryManager)
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

        public async Task<IActionResult> List()
        {
            var result = await mediator.Send(new GetBooksQuery());
            return View(result);
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
                    await libraryManager.IssueBookAsync(user, model.BookId);
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("", "Пользователя с таким Email не существует.");
            }
            return View(model);
        }

        public async Task<IActionResult> Take(int id)
        {
            await libraryManager.TakeAsync(id);
            return RedirectToAction("List");
        }
    }
}