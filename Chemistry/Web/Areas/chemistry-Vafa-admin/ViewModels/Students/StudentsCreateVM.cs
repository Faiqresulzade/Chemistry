using Microsoft.Build.Framework;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.Students
{
    public class StudentsCreateVM
    {
        public string? PhotoPath { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string University { get; set; }
        [Required]
        public string Point { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
