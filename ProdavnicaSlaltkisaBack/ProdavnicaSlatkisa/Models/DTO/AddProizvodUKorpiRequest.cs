namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class AddProizvodUKorpiRequest
    {
        public int ProizUkorpiId { get; set; }
        public int BrojKomada { get; set; }

        public int Idproizvoda { get; set; }

        public int KorpaId { get; set; }
    }
}
