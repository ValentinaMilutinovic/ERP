using System.ComponentModel.DataAnnotations;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class AddTipProizvodumRequest
    {
        [MaxLength(50, ErrorMessage = "The type cant be longer than 50 characters")]
        public string Sastav { get; set; } = null!;
    }
}
