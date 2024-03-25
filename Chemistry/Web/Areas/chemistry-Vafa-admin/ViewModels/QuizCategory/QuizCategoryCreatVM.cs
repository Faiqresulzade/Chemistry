using Microsoft.Build.Framework;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.QuizCategory
{
    public class QuizCategoryCreatVM
    {
        [Required] public string Name { get; set; }

        public string? PhotoPath { get; set; }

        [Required] public bool IsPaid { get; set; }

        [Required] public IFormFile Photo { get; set; }
    }
}
