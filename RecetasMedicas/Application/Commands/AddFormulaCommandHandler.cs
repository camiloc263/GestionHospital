using AutoMapper;
using CitasMedicas.Application.Commands;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasMedicas.Domain.Interfaces;
using RecetasMedicas.Domain.Entities;
using RecetasMedicas.Application.Services;

namespace RecetasMedicas.Application.Commands
{
	public class AddFormulaCommandHandler: IRequestHandler<AddFormulaCommand,bool>
	{
        private readonly IFormulaMedicaServices _formulaMedicaServices;
        private readonly IMapper _mapper;

        public AddFormulaCommandHandler(IFormulaMedicaServices formulaMedicaServices, IMapper mapper)
        {
            _formulaMedicaServices = formulaMedicaServices;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddFormulaCommand request, CancellationToken cancellationToken)
        {
            var formulaMedica = _mapper.Map<FormulaMedica>(request._formula);

            var formulaMedicaId = await _formulaMedicaServices.AddFormulaMedicaAsync(formulaMedica);
            return true;
        }
    }
}