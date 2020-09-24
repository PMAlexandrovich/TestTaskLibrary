using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Models;

namespace TestTaskLibrary.Controllers
{
    public class LibraryController : Controller
    {
        IBooksRepository booksRepository;
        LibraryManager libraryManager;
        UserManager<User> userManager;

        public LibraryController(IBooksRepository booksRepository, LibraryManager libraryManager, UserManager<User> userManager)
        {
            this.booksRepository = booksRepository;
            this.libraryManager = libraryManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public async Task<IActionResult> List()
        {
            ViewBag.User = await userManager.GetUserAsync(User);
            return View(booksRepository.Books.Include(b => b.BookStatus).ThenInclude(b => b.User).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Book(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                libraryManager.Book(user, id);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(int id)
        {
            libraryManager.Unbook(id);
            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
