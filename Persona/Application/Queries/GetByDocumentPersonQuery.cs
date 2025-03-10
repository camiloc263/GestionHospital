using MediatR;
using Persona.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persona.Application.Queries
{
	public class GetByDocumentPersonQuery: IRequest <PersonasDto>
	{
        public string _Identificacion { get; set; }

        public GetByDocumentPersonQuery(string Identificacion)
        {
            _Identificacion = Identificacion;
        }
    }
}