using Core.Entities;
using Web.ViewModels.News;

namespace Web.Services.Abstract
{
    public interface INewsService
    {
        Task<NewsIndexVM> News();
        Task<News> DetailsAsync(int id);

    }
}
