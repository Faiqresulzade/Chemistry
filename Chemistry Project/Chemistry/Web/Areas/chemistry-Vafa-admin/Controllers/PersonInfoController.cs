using Microsoft.AspNetCore.Mvc;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Area("chemistry-Vafa-admin")]
    public class PersonInfoController : Controller
    {
        private readonly IPersonInfoService _personInfo;

        public PersonInfoController(
            IPersonInfoService personInfo)
        {
            _personInfo = personInfo;
        }
        public async Task<IActionResult> Index()
        {
            var personInfo =await _personInfo.IndexAsync();
            //if(personInfo != null) return

            return View(personInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonInfoCreateVM model)
        {
            bool result =await _personInfo.CreateAsync(model);
            if (!result) return View(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
