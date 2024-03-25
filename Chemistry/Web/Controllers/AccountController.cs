using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using Web.Services.Abstract;
using Web.ViewModels.Account;
using Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService,
            UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (!ModelState.IsValid) return NotFound();
            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.surName,
                UserName = model.Email,
                CreatedAt=DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(model.Email, error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action("ConfrimUser", "Account", new { email = model.Email, token }, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());

            _emailService.Send(user.Email,"Account Confirmation", $"<a href = \"{link}\"> Click to confrim email.</a>");


            return RedirectToAction(nameof(VerifyEmail));
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if(!ModelState.IsValid)return View(model);
            var isExist = await _accountService.Login(model);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            if (isExist) return RedirectToAction("index", "home");
            return View(model); //NotFound(); 
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOut();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return NotFound();

            User exsistUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (exsistUser is null)
            {
                ModelState.AddModelError("Email", "Email isn't found");
                return View();
            }
            string body = string.Empty;
            string subject = "Verify Password Reset";



            string token = await _userManager.GeneratePasswordResetTokenAsync(exsistUser);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = exsistUser.Id, token = token }, HttpContext.Request.Scheme);

            body = $"<a href={link}>Reset Password</a>";

            body = body.Replace("{{fullname}}", exsistUser.Name);
            //await _emailService.Send(new AccountMailRequestVM { ToEmail = forgotPassword.Email, Subject = "ResetPassword", Body = $"<a href=\"{link}\">Reset Password</a>" });
            _emailService.Send(exsistUser.Email, subject, body);

            ModelState.AddModelError("Email","Emailinizi yoxlayın!");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId,string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();

            User user = await _userManager.FindByIdAsync(userId);
           
            if (user is null) return NotFound();

            return View(new AccountResetPasswordVM { Token = token, UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountResetPasswordVM resetPassword, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return BadRequest();

            if (!ModelState.IsValid)
                return View(resetPassword);

            User user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound();

            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(resetPassword);
            }

            return RedirectToAction(nameof(Login));
        }



        public async Task<IActionResult> ConfrimUser(string email, string token)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Email==email);

            if (user == null) return NotFound();

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect confrim");
                return RedirectToAction("Index", "Home");
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            return View();
        }
    }
}
