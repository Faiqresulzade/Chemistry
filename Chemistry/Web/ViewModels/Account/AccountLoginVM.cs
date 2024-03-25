using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountLoginVM
    {
        public AccountLoginVM()
        {
            Errors = new Dictionary<int, string>();
        }
        public Dictionary<int, string>? Errors { get; set; }

        [Microsoft.Build.Framework.Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
