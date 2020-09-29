using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Users;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(userManager.Users.Where(u => u.Email != "Admin").Select(u=>new UserViewModel() { Email = u.Email, FullName = u.FullName, Id = u.Id}).ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = model.Email, UserName = model.Email, FullName = model.FullName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role.ToString());
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                return View(new ChangePasswordViewModel() { Id = user.Id});
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                if(user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await userManager.UpdateAsync(user);
                            return RedirectToAction("List");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("password", error.Description);
                            }
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("List");
        }
    }
}