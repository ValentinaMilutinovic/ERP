using System.ComponentModel.DataAnnotations;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class AddAdministratorRequest
    {
        public int AdminId { get; set; }


        [MaxLength(13, ErrorMessage = "The jmbg cant be longer than 13 characters")]
        public string Jmbg { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "The Username cant be longer than 50 characters")]
        public string Username { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "The password cant be longer than 50 characters")]
        public string Lozinka { get; set; } = null!;
    }
}
