﻿using MediatR;
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
    public class DeleteReviewCommand : IRequest<int>
    {
        public int ReviewId { get; set; }

        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
        {
            private readonly IHttpContextAccessor contextAccessor;
            private readonly UserManager<User> userManager;
            private readonly IBookReviewsManager reviewsManager;

            public DeleteReviewCommandHandler(IHttpContextAccessor contextAccessor, IBookReviewsManager reviewsManager, UserManager<User> userManager)
            {
                this.contextAccessor = contextAccessor;
                this.reviewsManager = reviewsManager;
                this.userManager = userManager;
            }

            public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                if(user != null)
                {
                    return await reviewsManager.DeleteReviewAsync(user.Id, request.ReviewId);
                }
                return default;
            }
        }
    }
}
