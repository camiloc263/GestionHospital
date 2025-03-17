using AutoMapper;
using RecetasMedicas.Application.DTO;
using RecetasMedicas.Domain.Entities;

namespace RecetasMedicas.Infrastructure.Configuration
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<FormulaMedica, FormulaMedicaDTO>().ReverseMap();
        }
        
    }
}