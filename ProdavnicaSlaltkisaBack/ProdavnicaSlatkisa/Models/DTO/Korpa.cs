using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Korpa
    {
        public int KorpaId { get; set; }

        public int KorisnikId { get; set; }

        public int? UkupanIznos { get; set; }

        public int? BrProizvoda { get; set; }


        public virtual TblKorisnik Korisnik { get; set; } = null!;
    }
}
