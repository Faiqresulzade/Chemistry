using Core.Entities;
using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.VideoLesson;

namespace Web.Services.Concret
{
    public class VideoLessonService : IVideoLessonService
    {
        private readonly AppDbContext _appDbContext;

        private List<VideoLessonIndexVM> model = new List<VideoLessonIndexVM>();
        private bool ishaveCategory;


        public VideoLessonService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<VideoLessonIndexVM>> Index()
        {

            var videoLesson = new VideoLessonCategory()
            {
                VideoLessons = await _appDbContext.VideoLessons.Include(v => v.Category).ToListAsync(),
            };


            model = new List<VideoLessonIndexVM>();
            ishaveCategory = false;

            List<string> categories = new List<string>();

            foreach (var item in videoLesson.VideoLessons)
            {

                bool ishaveCategory = categories.Any(c => c == item.Category.CategoryTitle);

                if (!ishaveCategory)
                {
                    categories.Add(item.Category.CategoryTitle);
                }

                model.Add(new VideoLessonIndexVM()
                {
                    Id = item.Id,
                    ImageUrl = item.Photo,
                    Title = item.Title,
                    CategoryTitle = item.Category.CategoryTitle,
                    videoLessonCategories = categories,
                    IsPaid = item.İsPaid
                });
            }

            return model;
        }
        public async Task<VideoLesson> DetailsAsync(int id)
        {
            var videoLesson = await _appDbContext.VideoLessons.FindAsync(id);
            if (videoLesson == null) return null;
            return videoLesson;
        }
    }
}
