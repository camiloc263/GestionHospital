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

namespace CitasMedicas.Application.Queries
{
    public class GetCitasByIdQueryHandler : IRequestHandler<GetCitasByIdQuery, CitaDto>
	{
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly IMapper _mapper;

        public GetCitasByIdQueryHandler(ICitaMedicaRepository citaMedicaRepository, IMapper mapper)
        {
            _citaMedicaRepository = citaMedicaRepository;
            _mapper = mapper;
        }

        public async Task<CitaDto> Handle(GetCitasByIdQuery request, CancellationToken cancellationToken)
        {
            var citaMedica = await _citaMedicaRepository.GetById(request._Id);
            return _mapper.Map<CitaDto>(citaMedica);
        }
    }
}