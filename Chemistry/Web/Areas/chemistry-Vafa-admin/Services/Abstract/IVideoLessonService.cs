using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.VideoLesson;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IVideoLessonService
    {
        Task<VideoLessonIndexVM> IndexAsync();
        Task<VideoLessonCreateVM> CreateAsync();
        Task<bool> CreateAsync(VideoLessonCreateVM model);
        Task<VideoLessonUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id,VideoLessonUpdateVM model);
        Task<VideoLesson> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
