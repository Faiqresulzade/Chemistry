using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.ViewModels.User;
using Web.Services.Abstract;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-vafa-admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUsersService usersService,UserManager<User>userManager)
        {
            _usersService = usersService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()  
        {
            var model = new UserIndexVM()
            {
                Users = await _usersService.IndexAsync()
            };
            if (model == null)return NotFound();

            return View(model);
        }
        public async Task<IActionResult>Delete(string id)
        {
            bool exist=await _usersService.DeleteAsync(id);
            if(!exist) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
