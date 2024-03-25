using DataAcces.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Quiz;

namespace Web.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly AppDbContext _appDbContext;

        public QuizController(IQuizService quizService,AppDbContext dbContext)
        {
            _quizService = quizService;
            _appDbContext = dbContext;
        }

        public async Task<IActionResult> Index(int id)
        {
            var quizService = await _quizService.IndexAsync(id);
            if (quizService == null) return NotFound();

            if (quizService.Quizzes.Count == 0)
            {
                return NotFound();  
            }
            return View(quizService);
        }

        [HttpPost]
        public async Task<IActionResult> Index(QuizIndexVM model,int id)
        {
            model.IsFinished = true;

            if (model.Answers == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var quizService = await _quizService.CheckAnswerAsync(model, id);

            var errors = new Dictionary<int, string>();
            ViewData["Errors"] = quizService.Errors;


            return View(quizService);
        }
    }
}
