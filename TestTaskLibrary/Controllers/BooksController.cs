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
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Books;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = RoleTypes.Librarian)]
    public class BooksController : Controller
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator;
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
        [AllowAnonymous]
        public async Task<IActionResult> BookDatails(GetBookByIdQuery query)
        {
            var book = await mediator.Send(query);

            if (book != null)
            {
                return View(book);
            }

            return NotFound();
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
    }
}