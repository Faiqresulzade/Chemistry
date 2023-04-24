using DataAcces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;

namespace Web.ViewComponents
{
    public class NewsViewComponent: ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPersonInfoService _homeService;

        public NewsViewComponent(AppDbContext appDbContext,
            IPersonInfoService homeService)
        {
            _appDbContext = appDbContext;
            _homeService = homeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var news= await _appDbContext.News.ToListAsync();
            return View(news);
        }
    }
}
