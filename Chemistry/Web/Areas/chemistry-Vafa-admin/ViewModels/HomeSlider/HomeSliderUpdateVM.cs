using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.HomeSlider
{
    public class HomeSliderUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? BackRoundImage { get; set; }
        public string? BackRoundImagePath { get; set; }
        public DateTime ModefiedAt { get; set; }
    }
}
