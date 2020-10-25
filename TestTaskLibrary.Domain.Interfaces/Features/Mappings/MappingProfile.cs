using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(d => d.Author, s => s.MapFrom(s => s.Author.FullName))
                .ForMember(d => d.Genre, s => s.MapFrom(s => s.Genre.Name))
                .ForMember(d => d.CurrentStatus, s => s.MapFrom(s => s.CurrentBookStatus))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.CurrentBookStatus.Status))
                .ForMember(d => d.Reviews, s => s.MapFrom(s => s.BookAdditionalInfo.Reviews));

            CreateMap<Book, BookWithStatusesViewModel>()
                .ForMember(d => d.Author, s => s.MapFrom(s => s.Author.FullName))
                .ForMember(d => d.Genre, s => s.MapFrom(s => s.Genre.Name))
                .ForMember(d => d.CurrentStatus, s => s.MapFrom(s => s.CurrentBookStatus))
                .ForMember(d => d.Info, s => s.MapFrom(s => s.BookAdditionalInfo));

            CreateMap<BookAdditionalInfo, InfoViewModel>();
                

            CreateMap<BookReview, ReviewViewModel>()
                .ForMember(d => d.Book, s => s.MapFrom(s => s.BookAdditionalInfo.Book));

            CreateMap<BookStatus, StatusViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Genre, GenreViewModel>();
        }
    }
}
