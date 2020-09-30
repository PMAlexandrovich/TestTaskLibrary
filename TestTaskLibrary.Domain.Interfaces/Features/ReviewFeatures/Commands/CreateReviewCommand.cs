using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public CreateReviewCommand(int userId, int bookId, int rating, string content)
        {
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            Content = content;
        }

        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
        {
            private readonly IBookReviewsManager reviewsManager;

            public CreateReviewCommandHandler(IBookReviewsManager reviewsManager)
            {
                this.reviewsManager = reviewsManager;
            }

            public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                return await reviewsManager.AddReviewAsync(request.UserId, request.BookId, request.Rating, request.Content);
            }
        }
    }
}
