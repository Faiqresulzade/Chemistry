using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.VideoLesson
{
    public class VideoLessonUpdateVM
    {
        public int Id { get; set; }
        public string? PhotoPath { get; set; }
        [Required]
        public string VideoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }

        [Display(Name ="Ödənişlidirmi?")]
        public bool İspaid { get; set; }
    }
}
