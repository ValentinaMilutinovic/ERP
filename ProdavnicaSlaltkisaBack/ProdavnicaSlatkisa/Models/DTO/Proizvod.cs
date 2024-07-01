using ProdavnicaSlatkisa.API.Db;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class Proizvod
    {
        public int Idproizvoda { get; set; }
        public decimal Cena { get; set; }
        public int Idproizvodjaca { get; set; }
        public int TipProizvodaId { get; set; }
        public int KolicinaNaStanju { get; set; }
        public string Naziv { get; set; }

        public virtual TblProizvodjac IdproizvodjacaNavigation { get; set; } = null!;

        public virtual ICollection<TblProizvodUkorpi> TblProizvodUkorpis { get; } = new List<TblProizvodUkorpi>();
        public virtual TblTipProizvodum TipProizvoda { get; set; } = null!; 
    }
}
