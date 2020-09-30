using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Queries;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Components
{
    public class BookComments:ViewComponent
    {
        //IBookAdditionalInfosRepository repository;
        private readonly IBookReviewsRepository repository;
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public BookComments(IBookReviewsRepository repository, UserManager<User> userManager, IMediator mediator)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = await userManager.GetUserAsync(UserClaimsPrincipal);
            ViewBag.User = user;
            ViewBag.BookId = id;

            var reviews = await mediator.Send(new GetReviewsQuery() { BookId = id });

            ViewBag.CanWrite = reviews.Find(c => c.User.Id == user.Id) == null ? true : false;
            return View(reviews);
        }
    }
}
