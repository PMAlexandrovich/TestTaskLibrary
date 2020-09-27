using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Components
{
    public class BookComments:ViewComponent
    {
        //IBookAdditionalInfosRepository repository;
        IBookReviewsRepository repository;
        UserManager<User> userManager;

        public BookComments(IBookReviewsRepository repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = await userManager.GetUserAsync(UserClaimsPrincipal);
            ViewBag.User = user;
            ViewBag.BookId = id;
            var comments = repository.BookReviews.Where(c => c.BookAdditionalInfoId == id).Include(c => c.User).ToList();
            ViewBag.CanWrite = comments.Find(c => c.User == user) == null ? true : false;
            return View(comments);
        }
    }
}
