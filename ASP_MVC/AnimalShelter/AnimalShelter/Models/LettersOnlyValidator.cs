namespace AnimalShelter.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class LettersOnlyValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                // Checks for letters and spaces
                if (!Regex.IsMatch(stringValue, @"^[a-zA-Z\s]+$"))
                {
                    return new ValidationResult("This field can only contain letters and spaces.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
