using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;

namespace TestTaskLibrary.Domain.Application.Features.Library.Commands
{
    public class TakeBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }

        public class TakeBookCommandHandler : IRequestHandler<TakeBookCommand, bool>
        {
            private readonly ILibraryManager libraryManager;

            public TakeBookCommandHandler(ILibraryManager libraryManager)
            {
                this.libraryManager = libraryManager;
            }

            public async Task<bool> Handle(TakeBookCommand request, CancellationToken cancellationToken)
            {
                return await libraryManager.TakeAsync(request.BookId);
            }
        }
    }
}
