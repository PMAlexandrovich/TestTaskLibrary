using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Infrastructure.Data;

namespace TestTaskLibrary.Models
{
    public static class DBInitConfigurationExtension
    {
        public static void UseDbInit(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<CustomRole>>();
                    var context = services.GetRequiredService<LibraryContext>();
                    var mediator = services.GetRequiredService<IMediator>();
                    var environment = services.GetRequiredService<IWebHostEnvironment>();
                    IdentityInitializer.InitializeAsync(userManager, rolesManager, context, mediator, environment).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

        }
    }
}
