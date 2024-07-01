using System.ComponentModel.DataAnnotations;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class LoginAdminRequerst
    {
        public string Username { get; set; } = null!;

        public string Lozinka { get; set; } = null!;
    }
}
