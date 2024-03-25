using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class QuizCategoryController : Controller
    {
        private readonly IQuizCategoryService _quizCategoryService;

        public QuizCategoryController(IQuizCategoryService quizCategoryService)
        {
            _quizCategoryService = quizCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var quizCategory = await _quizCategoryService.Index();
            if (quizCategory == null) return View();
            return View(quizCategory);
        }
    }
}
