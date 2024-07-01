using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblKorisnik
{
    public int KorisnikId { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Email { get; set; }

    public string Kontakt { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public string Grad { get; set; } = null!;

    public virtual TblAdministrator? TblAdministrator { get; set; }

    public virtual ICollection<TblKorpa> TblKorpas { get; } = new List<TblKorpa>();

    public virtual TblKupac? TblKupac { get; set; }
}
