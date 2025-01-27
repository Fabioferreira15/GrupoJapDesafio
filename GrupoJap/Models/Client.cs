using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GrupoJap.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Indique o nome do cliente!")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Indique o Email do cliente!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indique o número de telemovel/telefone do cliente!")]
        [RegularExpression(@"^9[1236]\d{7}$|^2\d{8}$", ErrorMessage = "Número de telefone/telemovel invalido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tem de indicar a carta de codução do cliente")]
        public string DrivingLicense { get; set; }


        [ValidateNever]
        public ICollection<RentalContract> RentalContracts { get; set; }

    }
}
