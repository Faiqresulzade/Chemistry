using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.News
{
    public class NewsCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Info { get; set; }
        public string? PhotoPath { get; set; }
        [Required]
       public IFormFile Photo { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
