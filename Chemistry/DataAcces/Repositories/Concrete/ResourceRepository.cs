using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {

        public ResourceRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
