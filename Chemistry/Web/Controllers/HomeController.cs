using Microsoft.AspNetCore.Mvc;
using Web.Areas.chemistry_Vafa_admin.Services.Concrete;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IAccountService _accountService;
        private bool sendMessage;

        public HomeController(IHomeService homeService,
            IAccountService accountservice)
        {
            _homeService =homeService;
            _accountService = accountservice;
        }
        public async Task<IActionResult> Index()
        {
            var HomePage = await _homeService.IndexAsync();
            if(HomePage != null)  return View(HomePage);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(HomeMessageVM model)
        {
            sendMessage = true;
            var message = await _homeService.MessageAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}