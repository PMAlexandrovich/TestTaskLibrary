using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Controllers
{
    public class AuthorsController : GenericController<Author>
    {
        public AuthorsController(IGenericRepository<Author> repository) : base(repository)
        {
        }
    }
}