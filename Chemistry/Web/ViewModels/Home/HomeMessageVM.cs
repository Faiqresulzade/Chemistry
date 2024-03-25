using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.ViewModels.Home
{
    public class HomeMessageVM
    {
        [Required, MaxLength(17)]
        public string Name { get; set; }
        [Required, MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(5)]
        public string Message { get; set; }
    }
}
