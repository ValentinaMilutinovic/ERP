using ProdavnicaSlatkisa.API.Db;
using System.ComponentModel.DataAnnotations;

namespace ProdavnicaSlatkisa.API.Models.DTO
{
    public class AddKupacRequest
    {
        //public int KupacId { get; set; }

        [MaxLength(50,ErrorMessage ="The Username cant be longer than 50 characters")]
        public string UsernameRk { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "The Password cant be longer than 50 characters")]
        public string LozinkaRk { get; set; } = null!;

        //public int? BrojKupovina { get; set; }

        public bool? Registrovan { get; set; }
        //public virtual TblKorisnik Kupac { get; set; } = null!;

        public TblKorisnik Kupac { get; set; }
    }
}
