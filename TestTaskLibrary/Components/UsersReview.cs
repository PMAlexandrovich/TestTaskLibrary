using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Queries;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models.Reviews;

namespace TestTaskLibrary.Components
{
    public class UsersReview : ViewComponent
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public UsersReview(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = await userManager.GetUserAsync(UserClaimsPrincipal);
            var reviews = await mediator.Send(new GetReviewsQuery() { BookId = id, UserId = user.Id });
            if(reviews.Count != 0)
            {
                var review = reviews.FirstOrDefault();
                return View("Edit",review);
            }
            else
            {
                return View("Create", new CreateReviewViewModel() { BookId = id});
            }
            
        }
    }
}
