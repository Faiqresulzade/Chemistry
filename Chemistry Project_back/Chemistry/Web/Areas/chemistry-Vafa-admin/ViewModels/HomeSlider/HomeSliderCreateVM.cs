using Microsoft.Build.Framework;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.HomeSlider
{
    public class HomeSliderCreateVM
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile BackRoundImage { get; set; }
        public string? BackRoundImagePath { get; set; }
    }
}
