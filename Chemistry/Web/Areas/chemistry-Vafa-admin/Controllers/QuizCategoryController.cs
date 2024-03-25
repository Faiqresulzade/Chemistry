using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.QuizCategory;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class QuizCategoryController : Controller
    {
        private readonly IQuizCategoryService _quizCategoryService;

        public QuizCategoryController(IQuizCategoryService quizCategoryService)
        {
            _quizCategoryService = quizCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var quizCategory=await _quizCategoryService.IndexAsync();
            if (quizCategory == null) return View();
            return View(quizCategory);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(QuizCategoryCreatVM model)
        {
            bool isExist = await _quizCategoryService.CreateAsync(model);
            if (!isExist) return View(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Update(int id)
        {
            var quizCategory=await _quizCategoryService.UpdateAsync(id);
            if (quizCategory == null) return BadRequest();
            return View(quizCategory);
        }

        [HttpPost]
        public async Task<IActionResult>Update(int id,QuizCategoryUpdateVM model)
        {
            bool isExist=await _quizCategoryService.UpdateAsync(id,model);
            if(!isExist) return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>GetDelete(int id)
        {
            var quizCategory=await _quizCategoryService.GetDeleteAsync(id);
            if(quizCategory == null) return BadRequest();
            return View(quizCategory);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            bool isExist = await _quizCategoryService.DeleteAsync(id);
            if (!isExist) return BadRequest();
            return RedirectToAction(nameof(Index));
        }
    }
}
