using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-vafa-admin")]
    public class QuizAnswerController : Controller
    {
        private readonly IQuizAnswersRepository _quizAnswersRepository;

        public QuizAnswerController(IQuizAnswersRepository quizAnswersRepository)
        {
            _quizAnswersRepository = quizAnswersRepository;
        }

        public async Task<IActionResult> Index() => View(await _quizAnswersRepository.GetAllAsync());


        public async Task<IActionResult> Delete(int id)
        {
            var answer = await _quizAnswersRepository.GetAsync(id);
            if (answer == null) return BadRequest();

            await _quizAnswersRepository.DeleteAsync(answer);

            return RedirectToAction(nameof(Index));
        }
    }
}
