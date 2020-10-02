using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;

namespace TestTaskLibrary.Models.Reviews
{
    public class ReviewProfiler : Profile
    {
        public ReviewProfiler()
        {
            CreateMap<ReviewViewModel, CreateReviewViewModel>()
                .ForMember(d => d.BookId, s => s.MapFrom(s => s.BookAdditionalInfo.Book.Id));
        }
    }
}
