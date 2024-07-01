using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class ProizvodjacProfile : Profile
    {
        public ProizvodjacProfile()
        {
            CreateMap<Db.TblProizvodjac, Models.DTO.Proizvodjac>()
            .ReverseMap();
        }
    }
}
