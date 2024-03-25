using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
