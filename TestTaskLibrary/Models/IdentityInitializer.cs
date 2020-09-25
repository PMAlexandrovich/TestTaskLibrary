using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Data;

namespace TestTaskLibrary.Models
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, LibraryContext context, IBooksRepository booksRepository)
        {
            context.Database.Migrate();

            await CreateRoles(roleManager);

            await CreateUsers(userManager);

            CreateBooks(booksRepository);
        }

        private static void CreateBooks(IBooksRepository booksRepository)
        {
            Book book = new Book() { Author = "Лев Николаевич Толстой", Genre = "Роман", Title = "Война и мир" };
            booksRepository.Create(book);

            book = new Book() { Author = "Лев Николаевич Толстой", Genre = "Роман", Title = "Анна Каренина" };
            booksRepository.Create(book);

            book = new Book() { Author = "Александр Сергеевич Пушкин", Genre = "Роман в стихах", Title = "Евгений Онегин" };
            booksRepository.Create(book);

            book = new Book() { Author = "Александр Сергеевич Пушкин", Genre = "Поэма", Title = "Медный всадник" };
            booksRepository.Create(book);

            book = new Book() { Author = "Александр Сергеевич Пушкин", Genre = "Сказка", Title = "Сказка о рыбаке и рыбке" };
            booksRepository.Create(book);

            book = new Book() { Author = "Александр Сергеевич Пушкин", Genre = "Роман", Title = "Дубровский" };
            booksRepository.Create(book);

            book = new Book() { Author = "Рихтер Джеффри", Genre = "Языки программирования", Title = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#" };
            booksRepository.Create(book);

            book = new Book() { Author = "Фримен Адам", Genre = "Языки программирования", Title = "ASP.NET Core MVC 2 с примерами на C# для профессионалов" };
            booksRepository.Create(book);

            book = new Book() { Author = "Клири Стивен", Genre = "Языки программирования", Title = "Конкурентность в C#. Асинхронное, параллельное и многопоточное программирование" };
            booksRepository.Create(book);

            book = new Book() { Author = "Фёдор Михайлович Достоевский", Genre = "Роман", Title = "Преступление и наказание" };
            booksRepository.Create(book);

            book = new Book() { Author = "Николай Васильевич Гоголь", Genre = "Поэма", Title = "Мёртвые души" };
            booksRepository.Create(book);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
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
        }

        private static async Task CreateUsers(UserManager<User> userManager)
        {
            string login;
            string password;

            login = "Admin";
            password = "admin123";

            if (await userManager.FindByNameAsync(login) == null)
            {
                User admin = new User { UserName = login, Email = login, FullName = login };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            login = "Librarian";
            password = "lib123";

            if (await userManager.FindByNameAsync(login) == null)
            {
                User librarian = new User { UserName = login, Email = login, FullName = "Василий АП" };
                IdentityResult result = await userManager.CreateAsync(librarian, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarian, "Librarian");
                }
            }

            login = "ivan@mail.ru";
            password = "ivan123";

            if (await userManager.FindByNameAsync(login) == null)
            {
                User librarian = new User { UserName = login, Email = login, FullName = "Василий АП" };
                IdentityResult result = await userManager.CreateAsync(librarian, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarian, "Customer");
                }
            }

            login = "lera@gmail.com";
            password = "lera123";

            if (await userManager.FindByNameAsync(login) == null)
            {
                User librarian = new User { UserName = login, Email = login, FullName = "Василий АП" };
                IdentityResult result = await userManager.CreateAsync(librarian, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarian, "Customer");
                }
            }
        }
    }
}
