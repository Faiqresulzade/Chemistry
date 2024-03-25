using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class PersonInfoRepository:Repository<PersonInfo>,IPersonInfoRepository
    {
        public PersonInfoRepository(AppDbContext appDbContext):base(appDbContext){ }
    }
}
