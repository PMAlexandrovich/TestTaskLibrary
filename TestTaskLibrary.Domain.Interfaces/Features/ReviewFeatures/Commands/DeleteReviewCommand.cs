using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands
{
    public class DeleteReviewCommand : IRequest<int>
    {
        public int UserId { get; set; }

        public int ReviewId { get; set; }

        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
        {
            private readonly IBookReviewsManager reviewsManager;

            public DeleteReviewCommandHandler(IBookReviewsManager reviewsManager)
            {
                this.reviewsManager = reviewsManager;
            }

            public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
                return await reviewsManager.DeleteReviewAsync(request.UserId, request.ReviewId);
            }
        }
    }
}
