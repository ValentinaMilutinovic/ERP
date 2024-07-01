using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Administrator
    {

        public int AdminId { get; set; }

        public string Jmbg { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Lozinka { get; set; } = null!;

        public virtual TblKorisnik Admin { get; set; } = null!;
    }
}
