using MediatR;
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
	public class DeleteFormulaCommandHandler: IRequestHandler<DeleteFormulaCommand, bool>
	{

        private readonly IFormulaMedicaServices _formulaMedicaServices;
        public DeleteFormulaCommandHandler(IFormulaMedicaServices formulaMedicaServices)
        {
            _formulaMedicaServices = formulaMedicaServices;
        }

        public async Task<bool> Handle(DeleteFormulaCommand request, CancellationToken cancellationToken)
        {
            var formula = await _formulaMedicaServices.GetByCodigoRecetaAsync(request._codigo);
            if (formula == null)
            {
                return false; // No se encontró la persona
            }

            return await _formulaMedicaServices.DeleteByCodigoRecetaAsync(formula);
        }
    }
}