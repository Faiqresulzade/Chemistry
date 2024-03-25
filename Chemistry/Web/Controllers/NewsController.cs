using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public async Task<IActionResult> Index()
        {
            var news=await _newsService.News();
            if(news!=null) return View(news);
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var news = await _newsService.DetailsAsync(id);
            if (news == null) return BadRequest();
            return View(news);
        }
    }
}
