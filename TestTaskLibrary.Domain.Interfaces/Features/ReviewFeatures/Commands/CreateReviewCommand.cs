using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int BookId { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public CreateReviewCommand() { }
        public CreateReviewCommand(int bookId, int rating, string content)
        {
            BookId = bookId;
            Rating = rating;
            Content = content;
        }

        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
        {
            private readonly IBookReviewsManager reviewsManager;
            private readonly UserManager<User> userManager;
            private readonly IHttpContextAccessor contextAccessor;

            public CreateReviewCommandHandler(IBookReviewsManager reviewsManager, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
            {
                this.reviewsManager = reviewsManager;
                this.userManager = userManager;
                this.contextAccessor = contextAccessor;
            }

            public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                if(user != null)
                {
                    return await reviewsManager.AddReviewAsync(user.Id, request.BookId, request.Rating, request.Content);
                }
                return default;
            }
        }
    }
}
