using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            private IBooksRepository repository;

            public CreateBookCommandHandler(IBooksRepository repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {

                Book book = new Book()
                {
                    Author = request.Author,
                    Title = request.Title,
                    Genre = request.Genre,
                    BookAdditionalInfo = new BookAdditionalInfo(),
                    CurrentBookStatus = new BookStatus()
                };
                repository.Create(book);

                return book.Id;
            }
        }
    }
}
