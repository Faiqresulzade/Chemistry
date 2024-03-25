using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.VideoLesson
{
    public class VideoLessonCreateVM
    {
        public string? PhotoPath { get; set; }
        [Required]
        public string VideoPath { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreateAt { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        [Display(Name ="Ödənişlidirmi?")]
        public bool İsPaid { get; set; }
    }
}
