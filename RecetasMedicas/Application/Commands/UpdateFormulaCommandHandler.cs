using AutoMapper;
using MediatR;
using RecetasMedicas.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasMedicas.Domain.Interfaces;
using RecetasMedicas.Application.Services;

namespace RecetasMedicas.Application.Commands
{
	public class UpdateFormulaCommandHandler: IRequestHandler<UpdateFormulaCommand, bool>
	{
        private readonly IFormulaMedicaServices _formulaMedicaServices;


        public UpdateFormulaCommandHandler(IFormulaMedicaServices formulaMedicaServices)
        {
            _formulaMedicaServices = formulaMedicaServices;

        }

        public async Task<bool> Handle(UpdateFormulaCommand request, CancellationToken cancellationToken)
        {
            // Obtener la persona mediante algún método asíncrono (por ejemplo, GetByDocumentoAsync)
            var formulaMedicaEntitie = await _formulaMedicaServices.GetByCodigoRecetaAsync(request._codigo);
            if (formulaMedicaEntitie == null)
            {
                return false;
            }

            // Actualizar propiedades
            formulaMedicaEntitie.FechaEmision = request._formulaDto.FechaEmision;
            formulaMedicaEntitie.FechaVencimiento = request._formulaDto.FechaVencimiento;
            formulaMedicaEntitie.Estado = request._formulaDto.Estado;
            formulaMedicaEntitie.IdPaciente = request._formulaDto.IdPaciente;


           // Actualizar la persona en el repositorio
            await _formulaMedicaServices.UpdateAsync(formulaMedicaEntitie);
            return true;
        }
    }
}

