using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.News;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public async Task<IActionResult> Index()
        {
            var news = await _newsService.Index();
            return View(news);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(NewsCreateVM model)
        {
            var news = await _newsService.CreateAsync(model);
            if (news) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>Update(int id)
        {
            var news =await _newsService.UpdateAsync(id);
            if(news==null)return NotFound();
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult>Update(NewsUpdateVM model,int id)
        {
            bool isExist = await _newsService.UpdateAsync(model, id);
            if(isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>GetDelete(int id)
        {
            var news = await _newsService.GetDelete(id);
            if (news == null) return NotFound();
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            bool isExist = await _newsService.Delete(id);
            if (isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var news=await _newsService.DetailsAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }
    }
}
