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
    public class UnbookCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public class UnbookCommandHandler : IRequestHandler<UnbookCommand, bool>
        {
            private readonly UserManager<User> userManager;
            private readonly ILibraryManager libraryManager;

            public async Task<bool> Handle(UnbookCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByIdAsync(request.UserId.ToString());
                if(user != null)
                {
                    return await libraryManager.UnbookAsync(user, request.BookId);
                }
                return false;
            }
        }
    }
}
