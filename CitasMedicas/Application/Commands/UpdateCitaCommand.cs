using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Commands
{
	public class UpdateCitaCommand: IRequest<bool>
	{
        
        public CitaDto _citaDto;
        public int Id { get; set; }

        public UpdateCitaCommand( int id, CitaDto citaDto)
        {
            
            _citaDto = citaDto;
            Id = id;
        }
    }
}