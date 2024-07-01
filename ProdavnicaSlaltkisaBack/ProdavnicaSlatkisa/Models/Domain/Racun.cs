namespace ProdavnicaSlatkisa.API.Models.Domain
{
    public class Racun
    {
        public int RacunID { get; set; }
        public int UkupanIznos { get; set; }
        public DateOnly RacDatumKupovineunID { get; set; }
        public TimeOnly VremeKupovine { get; set; }
        public bool Popust { get; set; }
        public int ProcenatPop { get; set; }
        public int IznosSaPopustom { get; set; }

    }
}
