using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class TipProizvodumProfile : Profile
    {
        public TipProizvodumProfile()
        {
            CreateMap<Db.TblTipProizvodum, Models.DTO.TipProizvoda>()
    .ReverseMap();
        }
    }
}
