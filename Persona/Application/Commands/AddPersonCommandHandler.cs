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
using Persona.Application.Services;

namespace Persona.Application.Commands
{
	public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, bool>
    {
        private readonly IPersonaServices _personaService;
        private readonly IMapper _mapper;

        public AddPersonCommandHandler(IPersonaServices personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            var persona = _mapper.Map<ListaPersona>(request.PersonaDto);

            var personaId = await _personaService.AddAsync(persona);
            return true;
        }
    }
}