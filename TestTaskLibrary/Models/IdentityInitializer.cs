using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Models
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Librarian") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Librarian"));
            }
            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            string login = "Admin";
            string password = "admin123";

            if (await userManager.FindByNameAsync(login) == null)
            {
                User admin = new User { UserName = login, Email = login, FullName = login, Password = password };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            string loginl = "Librarian";
            string passwordl = "lib123";

            if (await userManager.FindByNameAsync(loginl) == null)
            {
                User librarian = new User { UserName = loginl, Email = loginl, FullName = "Василий АП", Password = passwordl };
                IdentityResult result = await userManager.CreateAsync(librarian, passwordl);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarian, "Librarian");
                }
            }
        }
    }
}
