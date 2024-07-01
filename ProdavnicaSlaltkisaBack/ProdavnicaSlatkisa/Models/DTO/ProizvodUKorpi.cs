using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class ProizvodUKorpi
    {
        public int ProizUkorpiId { get; set; }

        public int BrojKomada { get; set; }

        public int Iznos { get; set; }

        public int Idproizvoda { get; set; }

        public int KorpaId { get; set; }

        public virtual TblProizvod proizvodNavigation { get; set; } = null!;

        public virtual TblKorpa Korpa { get; set; } = null!;

    }
}
