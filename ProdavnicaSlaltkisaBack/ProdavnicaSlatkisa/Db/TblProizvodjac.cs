using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblProizvodjac
{
    public int Idproizvodjaca { get; set; }

    public string NazivProizvodjaca { get; set; } = null!;

    public string ZemljaPorekla { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvod { get; set; }
}
