using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Racun
    {
        public int RacunId { get; set; }

        public int UkupanIznos { get; set; }

        public DateTime DatumKupovine { get; set; }

        public TimeSpan VremeKupovine { get; set; }

        public bool? Popust { get; set; }

        public int? ProcenatPop { get; set; }

        public int? IznosSaPopustom { get; set; }

        public int KorpaId { get; set; }

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? Status { get; set; }

        public virtual TblKorpa Korpa { get; set; } = null!;
    }
}
