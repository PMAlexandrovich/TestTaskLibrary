using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Interfaces;
using System.Linq;
using AutoMapper;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Queries
{
    public class GetReviewsQuery : IRequest<List<ReviewViewModel>>
    {
        public int BookId { get; set; }

        public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<ReviewViewModel>>
        {
            private readonly IBookReviewsRepository reviewsRepository;
            private readonly IMapper mapper;

            public GetReviewsQueryHandler(IBookReviewsRepository reviewsRepository, IMapper mapper)
            {
                this.reviewsRepository = reviewsRepository;
                this.mapper = mapper;
            }

            public async Task<List<ReviewViewModel>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
            {
                var reviews = await reviewsRepository.GetAll.Include(r => r.User).Where(r => r.BookAdditionalInfo.BookId == request.BookId).ToListAsync();

                return mapper.Map<List<ReviewViewModel>>(reviews);
            }
        }
    }
}
