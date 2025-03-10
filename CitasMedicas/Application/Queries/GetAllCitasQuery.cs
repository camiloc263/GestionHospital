using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Queries
{
	public class GetAllCitasQuery : IRequest<List<CitaDto>>
    {
	}
}