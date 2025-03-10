using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persona.Application.Commands
{
	public class DeletePersonCommand: IRequest<bool>
	{
        public string _identificacion { get; set; }

        public DeletePersonCommand(string identificacion)
        {
            _identificacion = identificacion;
        }
    }
}