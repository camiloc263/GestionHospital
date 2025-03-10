using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Commands
{
	public class AddCitaCommand: IRequest<bool>
    {
        public CitaDto _citaDto { get; set; }

        public AddCitaCommand(CitaDto citaDto)
        {
            _citaDto = citaDto;
        }
    }
}