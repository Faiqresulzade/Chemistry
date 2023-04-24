using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo
{
    public class PersonInfoCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Info { get; set; }
        public string? PhotoPath{ get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
