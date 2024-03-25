using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.Resource
{
    public class ResourceUpdateVM
    {
        [Required] public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        public IFormFile? Pdf { get; set; }
        public string? PdfeUrl { get; set; }

        public string? Link { get; set; }
    }
}
