using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.QuizCategory
{
    public class QuizCategoryUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? PhotoPath { get; set; }

        public bool IsPaid { get; set; }

        public IFormFile? Photo { get; set; }
        public DateTime ModefiedAt { get; set; }
    }
}
