using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblProizvod
{
    public int IdProizvoda { get; set; }
    public decimal Cena { get; set; }
    public int Idproizvodjaca { get; set; }
    public int TipProizvodaId { get; set; }
    public int KolicinaNaStanju { get; set; }
    public string Naziv { get; set; }

    public virtual TblProizvodjac IdproizvodjacaNavigation { get; set; } 
    public virtual ICollection<TblProizvodUkorpi> TblProizvodUkorpis { get; } = new List<TblProizvodUkorpi>();
    public virtual TblTipProizvodum TipProizvoda { get; set; }
}
