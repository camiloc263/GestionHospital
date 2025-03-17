using MediatR;
using RecetasMedicas.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.Queries
{
	public class GetByIdQuery: IRequest<FormulaMedicaDTO>
	{
        public int _Id { get; set; }

        public GetByIdQuery(int id)
        {
            _Id = id;
        }
    }
}