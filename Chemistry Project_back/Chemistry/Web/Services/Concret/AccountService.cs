using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concret
{
    public class AccountService:IAccountService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
                                                IActionContextAccessor actionContextAccessor)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<HomeRegisterVM> Register(HomeRegisterVM model)
        {
            if (!_modelstate.IsValid) return null;
            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Surname=model.surName,

            };
            var result = await _userManager.CreateAsync(user, model.PassWord);
            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelstate.AddModelError(string.Empty, error.Description);
                }
                return null;
            }
            return model;
        }
        public async Task<HomeIndexVM> Login(HomeIndexVM model)
        {
            if (!_modelstate.IsValid) return null;
            var user = await _userManager.FindByNameAsync(model.LoginVM.Email);
            if (user == null)
            {
                _modelstate.AddModelError(string.Empty, "UserName or Password is InCorrect!!");
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.LoginVM.PassWord, false, false);
            if (!result.Succeeded)
            {
                _modelstate.AddModelError(string.Empty, "UserName or Password is InCorrect!!");
                return null;
            }
            return model;
        }
    }
}
