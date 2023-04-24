using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.Students
{
    public class StudentsUpdateVM
    {
        public int Id { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string University { get; set; }
        [Required]
        public string Point { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
