using System.ComponentModel.DataAnnotations;
using GrupoJap.Models;

public class EndDateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var rentalContract = (RentalContract)validationContext.ObjectInstance;

        if (value is DateTime endDate && rentalContract.StartDate != default)
        {
            if (endDate <= rentalContract.StartDate)
            {
                return new ValidationResult("A data de fim do aluguer deve ser posterior à data de início.");
            }
        }

        return ValidationResult.Success;
    }
}
