using AutoMapper;
using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Application.Services;

namespace CitasMedicas.Application.Queries
{
    public class GetCitasByIdQueryHandler : IRequestHandler<GetCitasByIdQuery, CitaDto>
	{
        private readonly ICitaMedicaServices _citaMedicaServices;
        private readonly IMapper _mapper;

        public GetCitasByIdQueryHandler(ICitaMedicaServices citaMedicaServices, IMapper mapper)
        {
            _citaMedicaServices = citaMedicaServices;
            _mapper = mapper;
        }

        public async Task<CitaDto> Handle(GetCitasByIdQuery request, CancellationToken cancellationToken)
        {
            var citaMedica = await _citaMedicaServices.GetById(request._Id);
            return _mapper.Map<CitaDto>(citaMedica);
        }
    }
}