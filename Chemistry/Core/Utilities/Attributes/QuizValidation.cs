using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Attributes
{

    public class QuizValidationAttribute : ValidationAttribute
    {
        public string CorrectVariant { get; set; }
        public QuizValidationAttribute(string correctVariant)
        {
            CorrectVariant = correctVariant;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var correctVariant = (string)value;

            // Add your custom validation logic here
            if (correctVariant != CorrectVariant)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
