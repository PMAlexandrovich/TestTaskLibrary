using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.ODdata;

namespace TestTaskLibrary.Controllers.OData
{
    [Authorize(AuthenticationSchemes = "Basic", Roles = "Admin")]
    public class UsersController : GenericController<User, UserViewModel>
    {
        private readonly UserManager<User> userManager;

        public UsersController(IMediator mediator, IGenericRepository<User> repository, IMapper mapper, UserManager<User> userManager) : base(mediator, repository, mapper)
        {
            this.userManager = userManager;
        }


        public IActionResult Get(int key)
        {
            return Ok(repository.GetAll().ProjectTo<UserViewModel>(mapper.ConfigurationProvider).FirstOrDefault(i => i.Id == key));
        }

        public async Task<IActionResult> Post([FromBody]AddUserApiModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = model.Email, UserName = model.Email, FullName = model.FullName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                    return Ok(user.Id);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int key, ODataActionParameters parameters)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == key);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, (string)parameters["Password"]);
                        if (result.Succeeded)
                        {
                            await userManager.UpdateAsync(user);
                            return Ok();
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

            return BadRequest(ModelState);
        }

        public async Task<ActionResult> Delete(int key)
        {
            User user = await userManager.FindByIdAsync(key.ToString());
            if (user != null)
            {
                if (await userManager.IsInRoleAsync(user, RoleTypes.Admin))
                {
                    return BadRequest();
                }
                await userManager.DeleteAsync(user);
                return Ok();
            }
            return NotFound();
        }
    }
}