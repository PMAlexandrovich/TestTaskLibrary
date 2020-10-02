using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands;
using TestTaskLibrary.Models.Reviews;

namespace TestTaskLibrary.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IMediator mediator;

        public ReviewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(new CreateReviewCommand(model.BookId, model.Rating, model.Content));
            }
            return RedirectToAction("BookDatails", "Books", new { id = model.BookId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteReviewCommand command, int bookId)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(command);
            }
            return RedirectToAction("BookDatails", "Books", new { id = bookId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ReviewId, int bookId)
        {
            ViewBag.bookId = bookId;
            return View(new EditReviewCommand() {  ReviewId = ReviewId});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewCommand command, int bookId)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(command);
            }
            return RedirectToAction("BookDatails", "Books", new { id = bookId });
        }

    }
}