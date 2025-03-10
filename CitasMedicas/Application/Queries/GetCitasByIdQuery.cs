using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Queries
{
	public class GetCitasByIdQuery : IRequest<CitaDto>
    {
        public int _Id { get; set; }

        public GetCitasByIdQuery(int id)
        {
            _Id = id;
        }
    }
}