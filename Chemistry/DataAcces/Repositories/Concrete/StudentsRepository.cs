using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class StudentsRepository:Repository<Students>,IStudentsRepository
    {
        public StudentsRepository(AppDbContext appDbContext):base(appDbContext){}
    }
}
