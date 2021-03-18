using System;
using System.ComponentModel.DataAnnotations;

namespace VideoRent.Utilities
{
    public class Min18YearsIfAMemberAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public Min18YearsIfAMemberAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            if (value == null)
            {
                return new ValidationResult(ErrorMessage);
            }

            if (!DateTime.TryParse(value.ToString(), out var birthDate))
            {
                return new ValidationResult(ErrorMessage);
            };

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (byte)property.GetValue(validationContext.ObjectInstance);

            int age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.DayOfYear > DateTime.Now.DayOfYear)
            {
                age--;
            }

            if (comparisonValue == 1)
            {
                return ValidationResult.Success;
            }
            if(age >= 18 && comparisonValue != 1)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}
