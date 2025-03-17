using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Repository;
using CitasMedicas.Application.Services;

namespace CitasMedicas.Application.Commands
{
	public class AddCitasCommandHandler:IRequestHandler<AddCitaCommand, bool>
	{
        private readonly ICitaMedicaServices _citaMedicaServices;
        private readonly IMapper _mapper;

        public AddCitasCommandHandler(ICitaMedicaServices citaMedicaServices, IMapper mapper)
        {
            _citaMedicaServices = citaMedicaServices;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddCitaCommand request, CancellationToken cancellationToken)
        {
            var citaMedica = _mapper.Map<CitaMedica>(request._citaDto);

            var citaMedicaId = await _citaMedicaServices.AddAsync(citaMedica);
            return true;
        }
    }
}