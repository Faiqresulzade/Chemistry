using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.HomeSlider;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]

    public class HomeSliderController : Controller
    {
        private readonly IHomeSliderService _homeSliderService;

        public HomeSliderController(IHomeSliderService homeSliderService)
        {
            _homeSliderService = homeSliderService;
        }
        public async Task<IActionResult> Index()
        {
            var homeSlider = await _homeSliderService.IndexAsync();
            return View(homeSlider);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeSliderCreateVM model)
        {
            bool isExist = await _homeSliderService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>Update(int id)
        {
            var slider=await _homeSliderService.UpdateAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]

        public async Task<IActionResult>Update(HomeSliderUpdateVM model, int id)
        {
            bool isExist = await _homeSliderService.UpdateAsync(id, model);
            if(!isExist) return View(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>GetDelete(int id)
        {
            var slider = await _homeSliderService.GetDelete(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]

        public async Task<IActionResult>Delete(int id)
        {

            bool isExist = await _homeSliderService.DeleteAsync(id);
            if (!isExist) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
