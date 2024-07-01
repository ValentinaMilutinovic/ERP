using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class ProizvodUKorpiProfile : Profile
    {
        public ProizvodUKorpiProfile()
        {
            CreateMap<Db.TblProizvodUkorpi, Models.DTO.ProizvodUKorpi>()
    .ReverseMap();
        }
    }
}
