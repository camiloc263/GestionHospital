using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using MediatR;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Application.Services;

namespace CitasMedicas.Application.Commands
{
	public class DeleteCitaCommandHandler: IRequestHandler <DeleteCitaCommand, bool>
    {
        ICitaMedicaServices _citaMedicaServices;

        public DeleteCitaCommandHandler(ICitaMedicaServices citaMedicaServices)
        {
            _citaMedicaServices = citaMedicaServices;
        }

        public async Task<bool> Handle(DeleteCitaCommand request, CancellationToken cancellationToken)
        {
            var cita = await _citaMedicaServices.GetById(request._id);
            if (cita == null)
            {
                return false; 
            }

            return await _citaMedicaServices.DeleteAsync(cita);
        }
    }
}