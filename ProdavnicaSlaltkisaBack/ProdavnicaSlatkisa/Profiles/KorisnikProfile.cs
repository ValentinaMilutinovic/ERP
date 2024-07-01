using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile()
        {
            CreateMap<Db.TblKorisnik, Models.DTO.Korisnik>()
    .ReverseMap();
        }
    }
}
