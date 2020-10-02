using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.Library.Commands
{
    public class IssueBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }

        public string UserEmail { get; set; }

        public class IssueBookCommandHandler : IRequestHandler<IssueBookCommand, bool>
        {
            private readonly UserManager<User> userManager;
            private readonly ILibraryManager libraryManager;

            public IssueBookCommandHandler(UserManager<User> userManager, ILibraryManager libraryManager)
            {
                this.userManager = userManager;
                this.libraryManager = libraryManager;
            }

            public async Task<bool> Handle(IssueBookCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.UserEmail);
                if(user != null)
                {
                    return await libraryManager.IssueBookAsync(user, request.BookId);
                }
                return false;
            }
        }
    }
}
