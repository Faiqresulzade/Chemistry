using System.ComponentModel.DataAnnotations;

namespace Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo
{
    public class PersonInfoUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Info { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
    }
}
