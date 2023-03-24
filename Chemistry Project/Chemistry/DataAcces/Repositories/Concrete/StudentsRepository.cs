using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Concrete
{
    public class StudentsRepository:Repository<Students>,IStudentsRepository
    {
        public StudentsRepository(AppDbContext appDbContext):base(appDbContext){}
    }
}
