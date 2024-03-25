using DataAcces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.VideoLesson;

namespace Web.Controllers
{
    public class VideoLessonController : Controller
    {
        private readonly IVideoLessonService _videoLesson;
        private readonly AppDbContext _appDbContext;

        public VideoLessonController(IVideoLessonService videoLesson,AppDbContext appDbContext)
        {
            _videoLesson = videoLesson;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var lesson = await _videoLesson.Index();
            if (lesson == null) return View();

            return View(lesson);
        }

        public async Task<IActionResult> LoadMore(int count)
        {
            var lesson = await _appDbContext.VideoLessons.
                                OrderByDescending(v => v.Id)
                                .Include(v => v.Category)
                                .Skip(3*count)
                                .Take(3)
                                .ToListAsync();

            return PartialView("_LessonPartial", lesson);

        }
        public async Task<IActionResult> Details(int id)
        {
            var model = new VideoLessonDetailsVM()
            {
                VideoLesson = await _videoLesson.DetailsAsync(id)
            };
            if (model.VideoLesson == null) return BadRequest();
            return View(model);
        }
        
    }
}
