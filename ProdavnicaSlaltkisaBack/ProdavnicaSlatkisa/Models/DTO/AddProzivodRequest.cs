namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class AddProzivodRequest
    {
        
        public decimal Cena { get; set; }

        public int Idproizvodjaca { get; set; }

        public int TipProizvodaId { get; set; }

        public int KolicinaNaStanju { get; set; }
        public string Naziv { get; set; }
    }
}
