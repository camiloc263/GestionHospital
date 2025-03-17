using MediatR;
using RecetasMedicas.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMedicas.Application.Queries
{
	public class GetByCodigoRecetaQuery: IRequest<FormulaMedicaDTO>
	{
        public string _codigoReceta{ get; set; }

        public GetByCodigoRecetaQuery(string codigoReceta)
        {
            _codigoReceta = codigoReceta;
        }
    }
}