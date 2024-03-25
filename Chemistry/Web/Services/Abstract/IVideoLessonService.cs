using Core.Entities;
using Web.ViewModels.VideoLesson;

namespace Web.Services.Abstract
{
    public interface IVideoLessonService
    {
       Task<List<VideoLessonIndexVM>>Index();
       // Task<List<VideoLessonIndexVM>> LoadMore();
        Task<VideoLesson> DetailsAsync(int id);
    }
}
