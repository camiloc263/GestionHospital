using AutoMapper;
using CitasMedicas.Application.DTO;
using CitasMedicas.Infrastructure.Repository;

namespace CitasMedicas.Infrastructure.configuration
{
	public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            // CreateMap<PersonasDto, ListaPersona>().ReverseMap().ForMember(dest => dest.numeroDocumento, opt => opt.Ignore());
            CreateMap<CitaMedica, CitaDto>().ReverseMap();



        }
    }
}