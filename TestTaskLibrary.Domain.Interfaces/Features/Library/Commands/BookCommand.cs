using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.Library.Commands
{
    public class BookCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public class BookCommandHandler : IRequestHandler<BookCommand, bool>
        {
            private readonly UserManager<User> userManager;
            private readonly ILibraryManager libraryManager;

            public BookCommandHandler(UserManager<User> userManager, ILibraryManager libraryManager)
            {
                this.userManager = userManager;
                this.libraryManager = libraryManager;
            }

            public async Task<bool> Handle(BookCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByIdAsync(request.UserId.ToString());
                if(user != null)
                {
                    return await libraryManager.BookAsync(user, request.BookId);
                }
                return false;
            }
        }
    }
}
