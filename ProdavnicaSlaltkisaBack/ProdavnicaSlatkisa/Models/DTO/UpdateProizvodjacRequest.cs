using System.ComponentModel.DataAnnotations;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class UpdateProizvodjacRequest
    {

        [MaxLength(50, ErrorMessage = "The name of brand cant be longer than 50 characters")]
        public string NazivProizvodjaca { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "The origin country cant be longer than 50 characters")]
        public string ZemljaPorekla { get; set; } = null!;
    }
}
