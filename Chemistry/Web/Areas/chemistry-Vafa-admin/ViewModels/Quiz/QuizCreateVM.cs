using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.Quiz
{
    public class QuizCreateVM
    {
        [Required]
        public string QuizTitle { get; set; }
        [Required]
        public string VariantA { get; set; }
        [Required]
        public string VariantB { get; set; }
        [Required]
        public string VariantC { get; set; }
        [Required]
        public string VariantD { get; set; }
        [Required]
        public string VariantE { get; set; }
        [Required]
        public string CorrectVariant { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int QuizCategoryId { get; set; }

        public DateTime CreateAt { get; set; }
        public List<SelectListItem>? Categories { get; set; }

        public IFormFile QuizImage { get; set; }
        public string? ImagePath { get; set; }

        public bool IsPaid { get; set; }
    }
}
