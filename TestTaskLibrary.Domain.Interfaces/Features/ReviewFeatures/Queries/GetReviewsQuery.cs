using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.ReviewFeatures.Queries
{
    public class GetReviewsQuery : IRequest<List<ReviewViewModel>>
    {
        public int? BookId { get; set; }

        public int? UserId { get; set; }

        public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<ReviewViewModel>>
        {
            private readonly IGenericRepository<BookReview> reviewsRepository;
            private readonly IMapper mapper;

            public GetReviewsQueryHandler(IGenericRepository<BookReview> reviewsRepository, IMapper mapper)
            {
                this.reviewsRepository = reviewsRepository;
                this.mapper = mapper;
            }

            public async Task<List<ReviewViewModel>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
            {
                var reviews = reviewsRepository.GetAll().Include(r => r.User).AsQueryable();

                if(request.BookId != null)
                {
                    reviews = reviews.Where(r => r.BookAdditionalInfo.BookId == request.BookId);
                }

                if (request.UserId != null)
                {
                    reviews = reviews.Where(r => r.UserId == request.UserId);
                }

                return mapper.Map<List<ReviewViewModel>>(await reviews.ToListAsync());
            }
        }
    }
}
