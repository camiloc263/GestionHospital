using AutoMapper;
using CitasMedicas.Application.DTO;
using CitasMedicas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using MediatR;

namespace CitasMedicas.Application.Queries
{
	public class GetAllCitasQueryHandler : IRequestHandler<GetAllCitasQuery, List<CitaDto>>
    {
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly IMapper _mapper;

        public GetAllCitasQueryHandler(ICitaMedicaRepository citaMedicaRepository, IMapper mapper)
        {
            _citaMedicaRepository = citaMedicaRepository;
            _mapper = mapper;
        }

        public async Task<List<CitaDto>> Handle(GetAllCitasQuery request, CancellationToken cancellationToken)
        {
            var citas = await _citaMedicaRepository.GetAll();
            return _mapper.Map<List<CitaDto>>(citas);
        }
    }
}