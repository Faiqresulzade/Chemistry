using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.News
{
    public class NewsUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Info { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
