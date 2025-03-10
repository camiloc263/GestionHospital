using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using MediatR;
using CitasMedicas.Domain.Interfaces;

namespace CitasMedicas.Application.Commands
{
	public class DeleteCitaCommandHandler: IRequestHandler <DeleteCitaCommand, bool>
    {
        private readonly ICitaMedicaRepository _citaRepository;

        public DeleteCitaCommandHandler(ICitaMedicaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public async Task<bool> Handle(DeleteCitaCommand request, CancellationToken cancellationToken)
        {
            var cita = await _citaRepository.GetById(request._id);
            if (cita == null)
            {
                return false; 
            }

            return await _citaRepository.DeleteAsync(cita);
        }
    }
}