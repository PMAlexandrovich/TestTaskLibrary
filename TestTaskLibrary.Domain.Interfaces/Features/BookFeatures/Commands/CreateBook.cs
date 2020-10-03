using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Поле Автор является обязательным")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Поле Название является обязательным")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Жанр является обязательным")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
        {
            private readonly IBooksRepository booksRepository;
            private readonly IAuthorRepository authorsRepository;
            private readonly IGenreRepository genresRepository;
            private readonly IBookStatusesRepository statusesRepository;

            public CreateBookCommandHandler(IBooksRepository booksRepository, IAuthorRepository authorsRepository, IGenreRepository genresRepository, IBookStatusesRepository statusesRepository)
            {
                this.booksRepository = booksRepository;
                this.authorsRepository = authorsRepository;
                this.genresRepository = genresRepository;
                this.statusesRepository = statusesRepository;
            }

            public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                var author = await authorsRepository.GetAll.FirstOrDefaultAsync(a => a.FullName == request.Author);
                if (author == null)
                {
                    author = new Author(request.Author);
                }

                var genre = await genresRepository.GetAll.FirstOrDefaultAsync(g => g.Name == request.Genre);
                if (genre == null)
                {
                    genre = new Genre(request.Genre);
                }

                Book book = new Book()
                {
                    Author = author,
                    Title = request.Title,
                    Genre = genre,
                    BookAdditionalInfo = new BookAdditionalInfo(),
                };
                await booksRepository.Create(book);
                var status = new BookStatus() { Book = book, Status = Status.Free };
                await statusesRepository.Create(status);
                book.CurrentBookStatus = status;
                await booksRepository.Update(book);

                return book.Id;
            }
        }
    }
}
