using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concret
{
    public class PersonInfoService : IPersonInfoService
    {
        private readonly AppDbContext _appDbContext;

        public PersonInfoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<HomeIndexVM> IndexAsync()
        {
            var model = new HomeIndexVM()
            {
                GetPersonInfo = await _appDbContext.PersonInfo.FirstOrDefaultAsync(),
                Students=await _appDbContext.Students.ToListAsync(),
                News=await _appDbContext.News
                                        .OrderByDescending(n=>n.Id)
                                        .Take(2)
                                        .ToListAsync(),
                HomeSliders=await _appDbContext.HomeSliders.ToListAsync(),
            };
            return model;
        }
    }
}
