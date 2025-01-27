using System.ComponentModel.DataAnnotations;

namespace GrupoJap.Validations
{
    public class ValidYearRange : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (!int.TryParse(value.ToString(), out int year))
            {
                return new ValidationResult("O valor deve ser um número válido");

            }

            var currentYear = DateTime.Now.Year;

            if (year < 1900 || year > currentYear)
            {
                return new ValidationResult($"O ano de fabrico deve estar entre 1900 e {currentYear}.");
            }

            return ValidationResult.Success;
        }


    }
}
