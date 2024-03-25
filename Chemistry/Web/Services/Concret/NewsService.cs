using Core.Entities;
using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.News;

namespace Web.Services.Concret
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _appDbContext;

        public NewsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<NewsIndexVM> News()
        {
            var model = new NewsIndexVM()
            {
                News = await _appDbContext.News.ToListAsync(),
            };
            return model;
        }
        public async Task<News> DetailsAsync(int id)
        {
            var news = await _appDbContext.News.FindAsync(id);
            return news;
        }

    }
}
