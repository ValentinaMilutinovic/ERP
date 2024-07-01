using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblAdministrator
{
    public int AdminId { get; set; }

    public string Jmbg { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public virtual TblKorisnik Korisnik{ get; set; } = null!;
}
