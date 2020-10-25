using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models.ODdata;

namespace TestTaskLibrary.Controllers.OData
{
    [Authorize(AuthenticationSchemes = "Basic", Roles = "Librarian")]
    public class AuthorsController : GenericController<Author, AuthorViewModel>
    {
        public AuthorsController(IMediator mediator, IGenericRepository<Author> repository, IMapper mapper) : base(mediator, repository, mapper)
        {
        }

        public IActionResult Get(int key)
        {
            return Ok(repository.GetAll().ProjectTo<AuthorViewModel>(mapper.ConfigurationProvider).FirstOrDefault(i => i.Id == key));
        }

        public async Task<IActionResult> Post([FromBody]AddAuthorApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new CreateAuthorCommand() { FullName = model.Name });
                if (result != 0)
                {
                    return Ok(result);
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Put(int key, [FromBody]AddAuthorApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new EditAuthorCommand() { Id = key, FullName = model.Name });
                if (result != 0)
                {
                    return Ok(result);
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var item = await repository.GetByIdAsync(key);
            if (item != null)
            {
                await repository.RemoveAsync(item);
                return Ok();
            }
            return BadRequest();
        }
    }
}