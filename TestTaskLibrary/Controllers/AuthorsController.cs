﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;
using TestTaskLibrary.Views.Authors;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = RoleTypes.Librarian)]
    public class AuthorsController : GenericController<Author,AuthorViewModel>
    {
        public AuthorsController(IGenericRepository<Author> repository, IMapper mapper, IMediator mediator) : base(repository, mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(new CreateAuthorCommand() { FullName = model.FullName });
                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await repository.GetByIdAsync(id);
            if(author != null)
            {
                return View(new AuthorEditViewModel() { Id = author.Id, FullName = author.FullName });
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(new EditAuthorCommand() { Id = model.Id, FullName = model.FullName });
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}