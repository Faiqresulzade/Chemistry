using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class HomeSliderRepository:Repository<HomeSlider>,IHomeSliderRepository
    {
        public HomeSliderRepository(AppDbContext appDbContext):base(appDbContext){}
    }
}
