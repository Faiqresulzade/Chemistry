using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
