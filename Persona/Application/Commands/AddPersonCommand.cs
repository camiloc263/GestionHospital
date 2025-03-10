using MediatR;
using Persona.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persona.Application.Commands
{
	public class AddPersonCommand : IRequest<bool>
    {
        public PersonasDto PersonaDto { get; set; }

        public AddPersonCommand(PersonasDto personaDto)
        {
            PersonaDto = personaDto;
        }
    }
}