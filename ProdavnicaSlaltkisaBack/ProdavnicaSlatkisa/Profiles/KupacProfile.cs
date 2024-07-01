using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class KupacProfile : Profile
    {
        public KupacProfile()
        {
            CreateMap<Db.TblKupac, Models.DTO.Kupac>()
    .ReverseMap();
        }
    }
}
