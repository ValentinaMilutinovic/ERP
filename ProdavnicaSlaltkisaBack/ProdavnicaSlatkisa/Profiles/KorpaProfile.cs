using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class KorpaProfile : Profile
    {
        public KorpaProfile()
        {
            CreateMap<Db.TblKorpa, Models.DTO.Korpa>()
    .ReverseMap();
        }
    }
}
