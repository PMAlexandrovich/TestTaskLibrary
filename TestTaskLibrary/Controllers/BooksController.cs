using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public BooksController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
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
            return View(new AddBookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = mapper.Map<CreateBookCommand>(model);
                int id = await mediator.Send(command);
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteBookCommand command)
        {
            int id = await mediator.Send(command);
            return RedirectToAction("List");
        }
    }
}