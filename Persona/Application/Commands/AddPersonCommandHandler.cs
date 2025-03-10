using AutoMapper;
using MediatR;
using Persona.Domain.Entities;
using Persona.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace Persona.Application.Commands
{
	public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public AddPersonCommandHandler(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            var persona = _mapper.Map<ListaPersona>(request.PersonaDto);

            var personaId = await _personaRepository.AddAsync(persona);
            return true;
        }
    }
}