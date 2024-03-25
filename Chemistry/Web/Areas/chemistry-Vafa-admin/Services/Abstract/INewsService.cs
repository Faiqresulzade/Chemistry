using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.News;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface INewsService
    {
        Task<NewsIndexVM> Index();
        Task<bool> CreateAsync(NewsCreateVM model);
        Task<NewsUpdateVM> UpdateAsync(int id);
        Task<bool>UpdateAsync(NewsUpdateVM model, int id);
        Task<News> GetDelete(int id);
        Task<bool>Delete(int id);
        Task<News> DetailsAsync(int id);
    }
}
