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

namespace CitasMedicas.Application.Commands
{
	public class AddCitasCommandHandler:IRequestHandler<AddCitaCommand, bool>
	{
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly IMapper _mapper;

        public AddCitasCommandHandler(ICitaMedicaRepository citaMedicaRepository, IMapper mapper)
        {
            _citaMedicaRepository = citaMedicaRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddCitaCommand request, CancellationToken cancellationToken)
        {
            var citaMedica = _mapper.Map<CitaMedica>(request._citaDto);

            var citaMedicaId = await _citaMedicaRepository.AddAsync(citaMedica);
            return true;
        }
    }
}