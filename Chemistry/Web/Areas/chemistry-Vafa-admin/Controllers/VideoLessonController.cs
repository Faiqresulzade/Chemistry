using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.VideoLesson;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class VideoLessonController : Controller
    {
        private readonly IVideoLessonService _videoLessonService;

        public VideoLessonController(IVideoLessonService videoLessonService)
        {
            _videoLessonService = videoLessonService;
        }
        public async Task<IActionResult> Index()
        {
            var lesson = await _videoLessonService.IndexAsync();
            if (lesson == null) return View();
            return View(lesson);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model=await _videoLessonService.CreateAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Create(VideoLessonCreateVM model)
        {
            var videoLesson = await _videoLessonService.CreateAsync(model);
            if(!videoLesson) return View(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
            var videoLesson = await _videoLessonService.UpdateAsync(id);
            if (videoLesson == null) return BadRequest();
            return View(videoLesson);
        }

        [HttpPost]
        public async Task<IActionResult>Update(int id,VideoLessonUpdateVM model)
        {
            if (id != model.Id)return BadRequest();
            var videoLesson=await _videoLessonService.UpdateAsync(id,model);
            if (!videoLesson) return View(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>GetDelete(int id)
        {
            var videoLesson=await _videoLessonService.GetDeleteAsync(id);
            if (videoLesson == null) return NotFound();
            return View(videoLesson);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var videoLesson=await _videoLessonService.DeleteAsync(id);
            if (!videoLesson) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
