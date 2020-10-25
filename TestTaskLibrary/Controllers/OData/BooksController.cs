using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.Library.Commands;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Books;

namespace TestTaskLibrary.Controllers.OData
{

    public class BooksController : GenericController<Book, BookViewModel>
    {
        private readonly UserManager<User> userManager;

        public BooksController(IMediator mediator, IGenericRepository<Book> repository, IMapper mapper, UserManager<User> userManager) : base(mediator, repository, mapper)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public override IActionResult Get()
        {
            return base.Get();
        }

        [AllowAnonymous]
        public IActionResult Get(int key)
        {
            return Ok(repository.GetAll().ProjectTo<BookViewModel>(mapper.ConfigurationProvider).FirstOrDefault(i => i.Id == key));
        }

        [EnableQuery]
        [AllowAnonymous]
        public IQueryable<ReviewViewModel> GetReviews([FromODataUri]int key)
        {
            return repository.GetAll().Where(b => b.Id == key).SelectMany(b => b.BookAdditionalInfo.Reviews).ProjectTo<ReviewViewModel>(mapper.ConfigurationProvider);
        }

        [AllowAnonymous]
        [EnableQuery]
        [ODataRoute("Books({key})/Reviews({id})")]
        public SingleResult<ReviewViewModel> GetReviews(int key, int id)
        {
            var result = repository.GetAll().Where(b => b.Id == key).SelectMany(b => b.BookAdditionalInfo.Reviews).Where(r => r.Id == id).ProjectTo<ReviewViewModel>(mapper.ConfigurationProvider);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post([FromBody]AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = mapper.Map<CreateBookCommand>(model);
                int id = await mediator.Send(command);
                return Ok(id);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic", Roles = RoleTypes.Customer)]
        public async Task<IActionResult> Book(int key)
        {
            var result = await mediator.Send(new BookCommand() { Id = key });
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic", Roles = RoleTypes.Customer)]
        public async Task<IActionResult> Unbook(int key)
        {
            var result = await mediator.Send(new UnbookCommand() { BookId = key });
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic", Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Issue([FromODataUri]int key, ODataActionParameters parameters)
        {
            var user = await userManager.FindByNameAsync((string)parameters["UserEmail"]);
            if (user != null)
            {
                if (await mediator.Send(new IssueBookCommand() { BookId = key, UserEmail = (string)parameters["UserEmail"] }))
                {
                    return Ok();
                }
            }
            else
            {
                ModelState.AddModelError("UserEmail", "Пользователь с таким Email не найден");
                return NotFound(ModelState);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic", Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Take(int key)
        {
            var result = await mediator.Send(new TakeBookCommand() { BookId = key });
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}