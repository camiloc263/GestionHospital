using AutoMapper;
using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Queries;
using CitasMedicas.Domain.Interfaces;
using MediatR;
using RecetasMedicas.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasMedicas.Domain.Interfaces;
using RecetasMedicas.Application.Services;

namespace RecetasMedicas.Application.Queries
{
    public class GetAllFormulaQueryHandler : IRequestHandler<GetAllFomulaQuery, List<FormulaMedicaDTO>>
	{
        private readonly IFormulaMedicaServices _formulaMedicaServices;
        private readonly IMapper _mapper;
        public GetAllFormulaQueryHandler(IFormulaMedicaServices formulaMedicaServices, IMapper mapper)
        {
            _formulaMedicaServices = formulaMedicaServices;
            _mapper = mapper;
        }

        public async Task<List<FormulaMedicaDTO>> Handle(GetAllFomulaQuery request, CancellationToken cancellationToken)
        {
            var citas = await _formulaMedicaServices.GetAllFormulasMedicas();
            return _mapper.Map<List<FormulaMedicaDTO>>(citas);
        }
    }
}