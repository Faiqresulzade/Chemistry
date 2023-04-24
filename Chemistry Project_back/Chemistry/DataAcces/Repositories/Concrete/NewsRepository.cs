using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class NewsRepository:Repository<News>, INewsRepository
    {
        public NewsRepository(AppDbContext appDbContext):base(appDbContext){}
    }
}
