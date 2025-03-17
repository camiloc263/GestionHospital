using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Commands
{
	public class FinalizarCitaCommand: IRequest<bool>
	{
        public int IdCita { get; }
        public RecetasDto Receta { get; }

        public FinalizarCitaCommand(int idCita, RecetasDto receta)
        {
            IdCita = idCita;
            Receta = receta;
        }
    }
}