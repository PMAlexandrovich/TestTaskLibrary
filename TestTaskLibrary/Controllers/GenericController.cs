using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Models;

namespace TestTaskLibrary.Controllers
{
    public class GenericController<TEntity,TViewModel> : Controller where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> repository;
        protected readonly IMediator mediator;
        protected readonly IMapper mapper;

        public GenericController(IGenericRepository<TEntity> repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IActionResult> List(int? pageIndex, int? pageSize)
        {
            var list = await mediator.Send(new GetListQuery<TEntity, TViewModel>() { PageIndex = pageIndex ??= 1, PageSize = pageSize ??= 20});
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            //return View(new CommonViewModel() { Model = list, ModelType = list.GetType()});
            return View(mapper.Map<List<TViewModel>>(list));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await repository.GetByIdAsync(id);
            if(item != null)
            {
                await repository.RemoveAsync(item);
            }
            return RedirectToAction("List");
        }

        public IActionResult Add()
        {
            return View();
        }

        
    }

    
}