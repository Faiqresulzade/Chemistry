using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Account;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace Web.Services.Concret
{
    public class AccountService:IAccountService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUrlHelper _Url;
        private readonly HttpContext _httpContext;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
                                                IActionContextAccessor actionContextAccessor,
                                               IUrlHelper urlHelperFactory, IHttpContextAccessor httpContext)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _userManager = userManager;
            _signInManager = signInManager;
            _Url = urlHelperFactory;
            _httpContext = httpContext.HttpContext;
        }

        [ValidateAntiForgeryToken]
        public async Task<bool> Register(AccountRegisterVM model)
        {
            if (!_modelstate.IsValid) return false;
            var user = new User
            {
                Email = model.Email,
                Name=model.Name,
                Surname=model.surName,
                UserName=model.Email
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelstate.AddModelError(string.Empty, error.Description);
                }
                return false;
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = _Url.Action("ConfrimUser", "Account", new { email = model.Email,token }, _httpContext.Request.Scheme,_httpContext.Request.Host.ToString());

            MailMessage message = new MailMessage("7l25x5f@code.edu.az", user.Email)
            {
                Subject = "Confrimation email",
                Body = $"<a href = \"{link}\"> Click to confrim email.</a>",
                IsBodyHtml = true
            };

            SmtpClient smtpClient = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("7L25x5f@code.edu.az", "tevtqiirkcyyiglb")
            };

            smtpClient.Send(message);
            return true;
        }

        public async Task<bool> Login(AccountLoginVM model)
        {
            if (!_modelstate.IsValid) return false;
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                model.Errors.Add(1, "UserName or Password is InCorrect!!");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.PassWord, false, false);
            if (!result.Succeeded)
            {
                model.Errors.Add(1, "UserName or Password is InCorrect!!");
                return false;
            }
            return true;
        }

        public async Task LogOut()
        {
          await _signInManager.SignOutAsync();
        }


        public async Task<bool> ResetPasswordAsync(AccountResetPasswordVM resetPasswordVM)
        {
            if (!_modelstate.IsValid)
            {
                _modelstate.AddModelError("Password", "Password is Required");
                return false;
            }

            var existUser = await _userManager.FindByIdAsync(resetPasswordVM.UserId);
            if (existUser == null)
            {
                _modelstate.AddModelError("Password", "User cannot found");
                return false;
            }

            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var checkedPassword = validateGuidRegex.IsMatch(resetPasswordVM.NewPassword);
            if (!checkedPassword)
            {
                _modelstate.AddModelError("Password", "Password was weak");
                return false;
            }

            if (await _userManager.CheckPasswordAsync(existUser, resetPasswordVM.NewPassword))
            {
                _modelstate.AddModelError("Password", "New password cant be same with old password");
                return false;
            }

            await _userManager.ResetPasswordAsync(existUser, resetPasswordVM.Token, resetPasswordVM.NewPassword);
            return true;
        }
    }
}
