using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Genres;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = RoleTypes.Librarian)]
    public class GenresController : GenericController<Genre, GenreViewModel>
    {
        public GenresController(IGenericRepository<Genre> repository, IMapper mapper, IMediator mediator) : base(repository, mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenreAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(new CreateGenreCommand() { Name = model.Name });
                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await repository.GetByIdAsync(id);
            if (author != null)
            {
                return View(new GenreEditViewModel() { Id = author.Id, Name = author.Name });
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(new EditGenreCommand() { Id = model.Id, Name = model.Name });
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}