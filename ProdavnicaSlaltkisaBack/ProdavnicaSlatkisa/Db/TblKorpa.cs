using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblKorpa
{
    public int KorpaId { get; set; }

    public int KorisnikId { get; set; }

    public int? UkupanIznos { get; set; }

    public int? BrProizvoda { get; set; }

    //public int? RacunId { get; set; }

    public virtual TblKorisnik Korisnik { get; set; } = null!;

    //public virtual TblRacun? Racun { get; set; }

    public virtual ICollection<TblProizvodUkorpi> TblProizvodUkorpis { get; } = new List<TblProizvodUkorpi>();

    public virtual ICollection<TblRacun> TblRacuns { get; } = new List<TblRacun>();
}
