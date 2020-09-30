using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Commands
{
    public class DeleteReviewCommand : IRequest<int>
    {
        public int UserId { get; set; }

        public int ReviewId { get; set; }

        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
        {
            IR

            public Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
