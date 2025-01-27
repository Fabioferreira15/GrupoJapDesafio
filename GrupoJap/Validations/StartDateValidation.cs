using System.ComponentModel.DataAnnotations;

namespace GrupoJap.Validations
{
    public class StartDateValidation : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if (value is DateTime startDate)
            {
                if (startDate < DateTime.Now.Date)
                {
                    return new ValidationResult("A data de inicio do aluguer não pode ser inferior à data atual.");
                }
            }

            return ValidationResult.Success;
        }

    }
}
