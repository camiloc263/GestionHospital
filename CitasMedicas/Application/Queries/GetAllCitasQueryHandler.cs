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
using CitasMedicas.Application.Services;

namespace CitasMedicas.Application.Queries
{
	public class GetAllCitasQueryHandler : IRequestHandler<GetAllCitasQuery, List<CitaDto>>
    {
        private readonly ICitaMedicaServices _citaMedicaServices;
        private readonly IMapper _mapper;

        public GetAllCitasQueryHandler(ICitaMedicaServices citaMedicaServices, IMapper mapper)
        {
            _citaMedicaServices = citaMedicaServices;
            _mapper = mapper;
        }

        public async Task<List<CitaDto>> Handle(GetAllCitasQuery request, CancellationToken cancellationToken)
        {
            var citas = await _citaMedicaServices.GetAll();
            return _mapper.Map<List<CitaDto>>(citas);
        }
    }
}