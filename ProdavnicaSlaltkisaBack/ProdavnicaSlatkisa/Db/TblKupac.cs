using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblKupac
{
    public int KupacId { get; set; }

    public string UsernameRk { get; set; } = null!;

    public string LozinkaRk { get; set; } = null!;

    public int? BrojKupovina { get; set; }

    public bool? Registrovan { get; set; }

    public virtual TblKorisnik Korisnik { get; set; } 
}
