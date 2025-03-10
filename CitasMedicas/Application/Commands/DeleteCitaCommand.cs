using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicas.Application.Commands
{
	public class DeleteCitaCommand: IRequest<bool>
	{
        public int _id { get; set; }

        public DeleteCitaCommand(int id)
        {
            _id = id;
        }

    }
}