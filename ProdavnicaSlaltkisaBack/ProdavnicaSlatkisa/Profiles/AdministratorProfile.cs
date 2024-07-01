using AutoMapper;

namespace ProdavnicaSlatkisa.API.Profiles
{
    public class AdministratorProfile : Profile
    {
        public AdministratorProfile()
        {
            CreateMap<Db.TblAdministrator, Models.DTO.Administrator>()
    .ReverseMap();
        }
    }
}
