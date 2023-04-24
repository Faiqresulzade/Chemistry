using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonInfoService _personInfoService;
        private readonly IAccountService _accountService;

        public HomeController(IPersonInfoService personInfoService,
            IAccountService accountservice)
        {
            _personInfoService = personInfoService;
            _accountService = accountservice;
        }
        public async Task<IActionResult> Index()
        {
            var HomePage = await _personInfoService.IndexAsync();
            if(HomePage != null)  return View(HomePage);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(HomeIndexVM model)
        {
            var isExist = await _accountService.Register(model.RegisterVM);
               if (isExist != null) return RedirectToAction(nameof(Index));
               return View(model);
        }
        //[HttpGet]
        //public async Task<IActionResult> Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(HomeRegisterVM model)
        //{
        //    var isExist = await _accountService.Register(model);
        //    if (isExist != null) return RedirectToAction(nameof(Login));
        //    return View(model);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Login(HomeLoginVM model)
        //{
        //    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //    {
        //        return Redirect(model.ReturnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("index", "home");
        //    }
        //    var isExist = await _accountService.Login(model);
        //    if (isExist != null) return RedirectToAction("index", "home");
        //    return View(model); //NotFound(); 
        //}
    }
}