using MediatR;
using Persona.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persona.Application.Queries
{
	public class GetAllPersonasQuery : IRequest<List<PersonasDto>>
    {
	}
}