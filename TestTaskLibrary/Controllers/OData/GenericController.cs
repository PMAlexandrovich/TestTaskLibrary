using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;

namespace TestTaskLibrary.Controllers.OData
{
    public class GenericController<TEntity, TViewModel> : ODataController where TEntity : class
    {
        protected readonly IMediator mediator;
        protected readonly IGenericRepository<TEntity> repository;
        protected readonly IMapper mapper;

        public GenericController(IMediator mediator, IGenericRepository<TEntity> repository, IMapper mapper)
        {
            this.mediator = mediator;
            this.repository = repository;
            this.mapper = mapper;
        }

        [EnableQuery]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().ProjectTo<TViewModel>(mapper.ConfigurationProvider));
        }



    }
}
