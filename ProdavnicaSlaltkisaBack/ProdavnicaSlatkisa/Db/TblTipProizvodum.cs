using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblTipProizvodum
{
    public int TipProizvodaId { get; set; }

    public string Sastav { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvodi { get; set; } 
}
