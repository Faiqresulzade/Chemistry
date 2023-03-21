using Core.Entities.Base;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbset;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbset = appDbContext.Set<T>();
        }
    }
}
