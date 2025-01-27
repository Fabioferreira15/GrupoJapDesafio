using System.ComponentModel.DataAnnotations;
using GrupoJap.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace GrupoJap.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }


        [Required(ErrorMessage = "Indique a marca da viatura!")]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Indique o modelo da viatura!")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Indique a matricula da viatura!")]
        [StringLength(50)]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Indique o ano da fabrico da viatura!")]
        public int ManufacturingYear { get; set; }

        [Required(ErrorMessage = "Indique o tipo de combustível da viatura!")]
        public FuelType FuelType { get; set; }

        public string Status { get; set; } = "Disponivel";

        [ValidateNever]
        public ICollection<RentalContract> RentalContracts { get; set; }
    }
}
