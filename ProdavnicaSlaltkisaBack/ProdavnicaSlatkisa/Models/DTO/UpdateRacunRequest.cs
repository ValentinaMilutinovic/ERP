namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class UpdateRacunRequest
    {
        public int UkupanIznos { get; set; }

        public DateTime DatumKupovine { get; set; }

        public TimeSpan VremeKupovine { get; set; }

        public bool? Popust { get; set; }

        public int? ProcenatPop { get; set; }

        public int? IznosSaPopustom { get; set; }

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }
        //
        // public int? KorpaId { get; set; }
    }
}
