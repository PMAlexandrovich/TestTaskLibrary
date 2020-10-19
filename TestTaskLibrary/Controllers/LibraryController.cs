using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.Library.Commands;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;

namespace TestTaskLibrary.Controllers
{
    [Authorize]
    public class LibraryController : GenericController<Book, BookViewModel>
    {
        private readonly UserManager<User> userManager;

        public LibraryController(IGenericRepository<Book> repository, IMediator mediator, IMapper mapper, UserManager<User> userManager) : base(repository, mediator, mapper)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.IsInRole(RoleTypes.Admin))
            {
                return RedirectToAction("List", "Users");
            }
            if (User.IsInRole(RoleTypes.Librarian))
            {
                return RedirectToAction("List", "Books");
            }
            return RedirectToAction("List");
        }

        [AllowAnonymous]
        public override async Task<IActionResult> List(int? pageIndex, int? pageSize, string search, string searchField, string sortField)
        {
            //var books = await mediator.Send(getBooksQuery);

            //if (search != null)
            //{
            //    switch (fieldSearch)
            //    {
            //        case FieldSearchType.Author:
            //            books = books.Where(b => b.Author?.ToLower().Contains(search.ToLower()) ?? false).ToList();
            //            break;
            //        case FieldSearchType.Title:
            //            books = books.Where(b => b.Title?.ToLower().Contains(search.ToLower()) ?? false).ToList();
            //            break;
            //        case FieldSearchType.Genre:
            //            books = books.Where(b => b.Genre?.ToLower().Contains(search.ToLower()) ?? false).ToList();
            //            break;
            //        default:
            //            books = books.Where(b => b.Title?.ToLower().Contains(search.ToLower()) ?? false).ToList();
            //            break;
            //    }
            //}
            //var bookViewModel = new LibraryListViewModel()
            //{
            //    Search = getBooksQuery.Search,
            //    SearchType = getBooksQuery.SearchType ?? FieldSearchType.Title,
            //    Books = books,
            //    PageIndex = getBooksQuery.PageIndex
            //};
            ViewData["User"] = await userManager.GetUserAsync(User);

            return await base.List(pageIndex, pageSize, search, searchField, sortField);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookCommand command, int? pageIndex, int? pageSize, string search, string searchField, string sortField)
        {
            await mediator.Send(command);
            return RedirectToAction("List", new { pageIndex, pageSize, search, searchField, sortField });
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(UnbookCommand command, int? pageIndex, int? pageSize, string search, string searchField, string sortField)
        {
            await mediator.Send(command);
            return RedirectToAction("List", new { pageIndex, pageSize, search, searchField, sortField });
        }

        [HttpGet]
        public async Task<IActionResult> Issue(GetBookByIdQuery query)
        {
            if (ModelState.IsValid)
            {
                var book = await mediator.Send(query);
                if (book != null)
                {
                    return View(new IssueBookCommand() { BookId = book.Id, UserEmail = book.CurrentStatus.User?.UserName });
                }
            }
            return RedirectToAction("List", "Books");
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Issue(IssueBookCommand command)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(command.UserEmail);
                if (user != null)
                {
                    if (await mediator.Send(command))
                    {
                        return RedirectToAction("List", "Books");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "Пользователь с таким Email не найден");
                }
            }
            return View(command);
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Librarian)]
        public async Task<IActionResult> Take(TakeBookCommand command, int? pageIndex, int? pageSize, string search, string searchField, string sortField)
        {
            await mediator.Send(command);
            return RedirectToAction("List", "Books", new { pageIndex, pageSize, search, searchField, sortField });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
