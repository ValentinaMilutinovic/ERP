namespace ProdavnicaSlatkisa.API.Models.Domain
{
    public class Kupac
    {
        public int KupacID { get; set; }
        public string UsernameRK { get; set; }
        public string LozinkaRk { get; set; }
        public int BrojKupovina { get; set; }
        public bool Registrovan { get; set; }
    }
}
