using DataAcces.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonInfoService _personInfoService;

        public HomeController(IPersonInfoService personInfoService)
        {
            _personInfoService = personInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var HomePage = await _personInfoService.IndexAsync();
            if(HomePage != null)  return View(HomePage);
            return View();
        }
    }
}