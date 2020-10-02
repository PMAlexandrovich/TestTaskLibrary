using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Queries
{
    public class GetReviewByIdQuery : IRequest<ReviewViewModel>
    {
        public int Id { get; set; }

        public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewViewModel>
        {
            private readonly IBookReviewsRepository reviewsRepository;
            private readonly IMapper mapper;

            public GetReviewByIdQueryHandler(IBookReviewsRepository reviewsRepository, IMapper mapper)
            {
                this.reviewsRepository = reviewsRepository;
                this.mapper = mapper;
            }

            public async Task<ReviewViewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
            {
                var review = await reviewsRepository.GetAll.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == request.Id,cancellationToken);

                return mapper.Map<ReviewViewModel>(review);
            }
        }
    }
}
