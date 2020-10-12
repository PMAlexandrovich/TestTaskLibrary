using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Models;

namespace TestTaskLibrary.Controllers
{
    public class GenericController<TEntity> : Controller where TEntity : class
    {
        private readonly IGenericRepository<TEntity> repository;

        public GenericController(IGenericRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public IActionResult List()
        {
            var list = repository.GetAll().ToList();
            //return View(new CommonViewModel() { Model = list, ModelType = list.GetType()});
            return View(list);
        }
    }

    
}