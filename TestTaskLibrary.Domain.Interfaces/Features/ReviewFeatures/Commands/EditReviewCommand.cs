using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands
{
    public class EditReviewCommand : IRequest<int>
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public class EditReviewCommandHandler : IRequestHandler<EditReviewCommand, int>
        {
            private readonly IHttpContextAccessor contextAccessor;
            private readonly UserManager<User> userManager;
            IBookReviewsManager reviewsManager;

            public EditReviewCommandHandler(IHttpContextAccessor contextAccessor, UserManager<User> userManager, IBookReviewsManager reviewsManager)
            {
                this.contextAccessor = contextAccessor;
                this.userManager = userManager;
                this.reviewsManager = reviewsManager;
            }

            public async Task<int> Handle(EditReviewCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                if(user != null)
                {
                    return await reviewsManager.EditReviewAsync(user.Id, request.ReviewId, request.Rating, request.Content);
                }
                return default;
            }
        }
    }
}
