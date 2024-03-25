using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Quiz;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        public async Task<IActionResult> Index()
        {
            var quizzes = await _quizService.IndexAsync();
            if (quizzes == null) return View();
            return View(quizzes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var quizzes = await _quizService.CreateAsync();
            if (quizzes == null) return View();
            return View(quizzes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuizCreateVM model)
        {
            bool isExist = await _quizService.CreateAsync(model);
            if (!isExist) return View(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
            var quiz = await _quizService.Update(id);
            if (quiz == null) return BadRequest();
            return View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult>Update(int id, QuizUpdateVM model)
        {
            bool isExist = await _quizService.Update(id, model);

            if (!isExist) return View(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>GetDelete(int id)
        {
            Quiz quiz = await _quizService.GetdeleteAsync(id);
            if (quiz == null) return BadRequest();
            return View(quiz);  
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            bool isExist = await _quizService.DeleteAsync(id);
            if (!isExist) return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var quiz = await _quizService.Details(id);
            if (quiz == null) return BadRequest();
            return View(quiz);
        }
    }
}
