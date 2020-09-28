using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands
{
    public class DeleteBookCommand:IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
        {
            IBooksRepository repository;

            public DeleteBookCommandHandler(IBooksRepository repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                repository.Delete(request.Id);
                return request.Id;
            }
        }
    }
}
