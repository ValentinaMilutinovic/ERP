using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblProizvodUkorpi
{
    public int ProizUkorpiId { get; set; }

    public int BrojKomada { get; set; }

    public int Iznos { get; set; }

    public int ProizvodId { get; set; }

    public int KorpaId { get; set; }

    public virtual TblProizvod ProizvodNavigation { get; set; } = null!;

    public virtual TblKorpa Korpa { get; set; } = null!;
}
