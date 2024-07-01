using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Kupac
    {
        public int KupacId { get; set; }

        public string UsernameRk { get; set; } = null!;

        public string LozinkaRk { get; set; } = null!;

        public int? BrojKupovina { get; set; }

        public bool? Registrovan { get; set; }

        public int KorisnikId { get; set; }


        public virtual TblKorisnik Korisnik { get; set; } = null!; 
    }
}
