using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models.ODdata;

namespace TestTaskLibrary.Controllers.OData
{
    [Authorize(AuthenticationSchemes = "Basic", Roles = "Librarian")]
    public class GenresController : GenericController<Genre, GenreViewModel>
    {
        public GenresController(IMediator mediator, IGenericRepository<Genre> repository, IMapper mapper) : base(mediator, repository, mapper)
        {
        }

        public IActionResult Get(int key)
        {
            return Ok(repository.GetAll().ProjectTo<GenreViewModel>(mapper.ConfigurationProvider).FirstOrDefault(i => i.Id == key));
        }

        public async Task<IActionResult> Post([FromBody]AddGenreApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new CreateGenreCommand() { Name = model.Name });
                if (result != 0)
                {
                    return Ok(result);
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Put(int key, [FromBody]AddGenreApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new EditGenreCommand() { Id = key, Name = model.Name });
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