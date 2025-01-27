using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GrupoJap.Models
{
    public class RentalContract
    {
        [Key]
        public int RentalContractId { get; set; }

        [Required(ErrorMessage = "Tem de associar um cliente!")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Tem de associar uma viatura!")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Indique a data de inicio do contrato!")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Indique a data de fim do contrato!")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Indique a quilometragem inicial!")]
        public int InitialKilometers { get; set; }

        [ValidateNever]
        public Client Client { get; set; }

        [ValidateNever]
        public Vehicle Vehicle { get; set; }



    }
}
