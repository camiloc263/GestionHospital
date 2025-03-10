using MediatR;
using Persona.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persona.Application.Commands
{
	public class UpdatePersonCommand : IRequest<bool>
    {
        public string Identificacion { get; set; }
        public PersonasDto _personaDto;
        public UpdatePersonCommand(string identificacion, PersonasDto personaDto)
        {
            Identificacion = identificacion;
            _personaDto = personaDto;
        }
    }
}