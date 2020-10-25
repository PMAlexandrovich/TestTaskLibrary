using AutoMapper;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models.ODdata;

namespace TestTaskLibrary.Controllers.OData
{
    public class ReviewsController : GenericController<BookReview, ReviewViewModel>
    {
        public ReviewsController(IMediator mediator, IGenericRepository<BookReview> repository, IMapper mapper) : base(mediator, repository, mapper)
        {
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Basic")]
        [ODataRoute("Books({key})/Reviews")]
        public async Task<IActionResult> Post([FromODataUri]int key, [FromBody]AddReviewApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new CreateReviewCommand(key, model.Rating, model.Content));
                if (result != 0)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Basic")]
        public async Task<IActionResult> Delete(int key)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new DeleteReviewCommand() { ReviewId = key, });
                if (result != 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Basic")]
        public async Task<IActionResult> Put([FromODataUri]int key, [FromBody]EditReviewApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new EditReviewCommand() { ReviewId = key, Content = model.Content, Rating = model.Rating });
                if (result != 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}