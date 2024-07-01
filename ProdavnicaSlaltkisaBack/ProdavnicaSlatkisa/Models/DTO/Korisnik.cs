using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Korisnik
    {
        public int KorisnikId { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public string? Email { get; set; }

        public string Kontakt { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public string Grad { get; set; } = null!;
        /*
        public virtual TblAdministrator? TblAdministrator { get; set; }


        public virtual TblKupac? TblKupac { get; set; }*/
    }
}
