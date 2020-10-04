using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;

namespace TestTaskLibrary.Models.Books
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookViewModel, CreateBookCommand > ().ReverseMap();
        }
    }
}
