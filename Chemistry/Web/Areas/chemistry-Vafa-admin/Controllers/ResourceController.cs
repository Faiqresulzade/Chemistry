using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Resource;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _resourceService.IndexAsync();

            return View(model);
        }

        public async Task<IActionResult> Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(ResourceCreateVM model)
        {
            bool result = await _resourceService.CreateAsync(model);

            if (result)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            ResourceUpdateVM model = await _resourceService.UpdateAsync(id);
            if (model != null) return View(model);

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ResourceUpdateVM model)
        {
            var result = await _resourceService.UpdateAsync(id, model);
            if (result) return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> GetDelete(int id)
        {
            var model = await _resourceService.GetDeleteAsync(id);
            if (model != null) return View("Delete",model);

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _resourceService.DeleteAsync(id);

            if (result) return RedirectToAction("Index");

            return BadRequest();
        }
    }
}
