using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string ImageName { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
        {
            private readonly IGenericRepository<Book> booksRepository;
            private readonly IGenericRepository<Author> authorsRepository;
            private readonly IGenericRepository<Genre> genresRepository;
            private readonly IGenericRepository<BookStatus> statusesRepository;

            public CreateBookCommandHandler(IGenericRepository<Book> booksRepository, IGenericRepository<Author> authorsRepository, IGenericRepository<Genre> genresRepository, IGenericRepository<BookStatus> statusesRepository)
            {
                this.booksRepository = booksRepository;
                this.authorsRepository = authorsRepository;
                this.genresRepository = genresRepository;
                this.statusesRepository = statusesRepository;
            }

            public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                var author = await authorsRepository.GetAll().FirstOrDefaultAsync(a => a.FullName == request.Author);
                if (author == null)
                {
                    author = new Author(request.Author);
                }

                var genre = await genresRepository.GetAll().FirstOrDefaultAsync(g => g.Name == request.Genre);
                if (genre == null)
                {
                    genre = new Genre(request.Genre);
                }

                Book book = new Book()
                {
                    Author = author,
                    Title = request.Title,
                    Genre = genre,
                    ImageName = request.ImageName,
                    BookAdditionalInfo = new BookAdditionalInfo(),
                };
                await booksRepository.AddAsync(book);
                var status = new BookStatus() { Book = book, Status = Status.Free };
                await statusesRepository.AddAsync(status);
                book.CurrentBookStatus = status;
                await booksRepository.UpdateAsync(book);

                return book.Id;
            }
        }
    }
}
