using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Students:BaseEntity
    {
        public string Photo { get; set; }
        public string Profession { get; set; }
        public string FullName { get; set; }
        public string University { get; set; }
        public string Point { get; set; }
    }
}
