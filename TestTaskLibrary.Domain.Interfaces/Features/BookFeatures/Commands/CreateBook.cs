using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
        {
            private readonly IBooksRepository booksRepository;

            private readonly IAuthorRepository authorsRepository;

            private readonly IGenreRepository genresRepository;

            public CreateBookCommandHandler(IBooksRepository booksRepository, IAuthorRepository authorsRepository, IGenreRepository genresRepository)
            {
                this.booksRepository = booksRepository;
                this.authorsRepository = authorsRepository;
                this.genresRepository = genresRepository;
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
                    CurrentBookStatus = new BookStatus()
                };
                await booksRepository.Create(book);

                return book.Id;
            }
        }
    }
}
