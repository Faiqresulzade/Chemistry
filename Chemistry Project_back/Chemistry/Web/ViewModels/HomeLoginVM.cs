using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class HomeLoginVM
    {

        [Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
