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
            var PersonInfo = await _personInfoService.IndexAsync();
          if(PersonInfo!=null)  return View(PersonInfo);
          return View();
        }
    }
}