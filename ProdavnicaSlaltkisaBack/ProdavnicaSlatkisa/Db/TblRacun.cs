using System;
using System.Collections.Generic;

namespace ProdavnicaSlatkisa.API.Db;

public partial class TblRacun
{
    public int RacunId { get; set; }

    public int UkupanIznos { get; set; }

    public DateTime DatumKupovine { get; set; }

    public TimeSpan VremeKupovine { get; set; }

    public bool? Popust { get; set; }

    public int? ProcenatPop { get; set; }

    public int? IznosSaPopustom { get; set; }

    public int KorpaId { get; set; }

    public string? ClientSecret { get; set; }

    public string? PaymentIntentId { get; set; }

    public string? Status { get; set; }


    public virtual TblKorpa Korpa { get; set; }

}
