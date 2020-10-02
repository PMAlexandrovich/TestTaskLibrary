using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class BookComments : ViewComponent
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
            var reviews = await mediator.Send(new GetReviewsQuery() { BookId = id });
            if (user != null)
            {
                reviews = reviews.Where(r => r.User.Id != user.Id).ToList();
            }
            

            return View(reviews);
        }
    }
}
