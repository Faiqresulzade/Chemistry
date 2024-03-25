using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Curseİnfo:BaseEntity
    {
        [Range(0, int.MaxValue, ErrorMessage = "Müsbət ədəd daxil edin!")]
        public int Graduates { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Müsbət ədəd daxil edin!")]
        public int Test { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Müsbət ədəd daxil edin!")]
        public int VideoLesson { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Müsbət ədəd daxil edin!")]
        public int Experience { get; set; }
    }
}
