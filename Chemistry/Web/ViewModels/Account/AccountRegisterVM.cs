using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.ViewModels.Account
{
    public class AccountRegisterVM
    {

        [Required, MaxLength(17)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string surName { get; set; }
        [Required, MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(30), DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required, MaxLength(30), DataType(DataType.Password), Display(Name = "Confirm PassWord"), Compare(nameof(PassWord))]
        public string ConfirmPassWord { get; set; }
    }
}
