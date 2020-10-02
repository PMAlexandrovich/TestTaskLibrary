using MediatR;
using Microsoft.AspNetCore.Http;
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
        public int Id { get; set; }

        public class BookCommandHandler : IRequestHandler<BookCommand, bool>
        {
            private readonly UserManager<User> userManager;
            private readonly IHttpContextAccessor accessor;
            private readonly ILibraryManager libraryManager;
            

            public BookCommandHandler(UserManager<User> userManager, IHttpContextAccessor accessor, ILibraryManager libraryManager)
            {
                this.userManager = userManager;
                this.accessor = accessor;
                this.libraryManager = libraryManager;
            }

            public async Task<bool> Handle(BookCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.GetUserAsync(accessor.HttpContext.User);
                if(user != null)
                {
                    return await libraryManager.BookAsync(user, request.Id);
                }
                return false;
            }
        }
    }
}
