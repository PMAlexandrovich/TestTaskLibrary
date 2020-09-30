﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Data;

namespace TestTaskLibrary.Models
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<CustomRole> roleManager, LibraryContext context, IMediator mediator)
        {
            context.Database.Migrate();

            await CreateRoles(roleManager);

            await CreateUsers(userManager);

            await CreateBooks(mediator);
        }

        private static async Task CreateBooks(IMediator mediator)
        {
            var books = await mediator.Send(new GetBooksQuery());
            if(books.Count == 0)
            {
                await mediator.Send( new CreateBookCommand() { Author = "Лев Николаевич Толстой", Genre = "Роман", Title = "Война и мир" });

                await mediator.Send( new CreateBookCommand() { Author = "Лев Николаевич Толстой", Genre = "Роман", Title = "Анна Каренина" });

                await mediator.Send( new CreateBookCommand() { Author = "Александр Сергеевич Пушкин", Genre = "Роман в стихах", Title = "Евгений Онегин" });

                await mediator.Send( new CreateBookCommand() { Author = "Александр Сергеевич Пушкин", Genre = "Поэма", Title = "Медный всадник" });

                await mediator.Send( new CreateBookCommand() { Author = "Александр Сергеевич Пушкин", Genre = "Сказка", Title = "Сказка о рыбаке и рыбке" });

                await mediator.Send( new CreateBookCommand() { Author = "Александр Сергеевич Пушкин", Genre = "Роман", Title = "Дубровский" });

                await mediator.Send( new CreateBookCommand() { Author = "Рихтер Джеффри", Genre = "Языки программирования", Title = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#" });

                await mediator.Send( new CreateBookCommand() { Author = "Фримен Адам", Genre = "Языки программирования", Title = "ASP.NET Core MVC 2 с примерами на C# для профессионалов" });

                await mediator.Send( new CreateBookCommand() { Author = "Клири Стивен", Genre = "Языки программирования", Title = "Конкурентность в C#. Асинхронное, параллельное и многопоточное программирование" });

                await mediator.Send( new CreateBookCommand() { Author = "Фёдор Михайлович Достоевский", Genre = "Роман", Title = "Преступление и наказание" });

                await mediator.Send( new CreateBookCommand() { Author = "Николай Васильевич Гоголь", Genre = "Поэма", Title = "Мёртвые души" });
            }
        }

        private static async Task CreateRoles(RoleManager<CustomRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new CustomRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Librarian") == null)
            {
                await roleManager.CreateAsync(new CustomRole("Librarian"));
            }
            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new CustomRole("Customer"));
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
                User librarian = new User { UserName = login, Email = login, FullName = "Василий А.П." };
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
                User librarian = new User { UserName = login, Email = login, FullName = "Иван П.П." };
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
                User librarian = new User { UserName = login, Email = login, FullName = "Валерия В.А." };
                IdentityResult result = await userManager.CreateAsync(librarian, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarian, "Customer");
                }
            }
        }
    }
}
