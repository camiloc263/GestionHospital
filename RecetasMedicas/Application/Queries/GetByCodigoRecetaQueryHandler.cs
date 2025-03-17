using AutoMapper;
using MediatR;
using RecetasMedicas.Application.DTO;
using RecetasMedicas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasMedicas.Application.Services;

namespace RecetasMedicas.Application.Queries
{
	public class GetByCodigoRecetaQueryHandler: IRequestHandler<GetByCodigoRecetaQuery, FormulaMedicaDTO>
	{
        private readonly IFormulaMedicaServices _formulaMedicaServices;
        private readonly IMapper _mapper;

        public GetByCodigoRecetaQueryHandler(IFormulaMedicaServices formulaMedicaServices, IMapper mapper)
        {
            _formulaMedicaServices = formulaMedicaServices;
            _mapper = mapper;
        }

        public async Task<FormulaMedicaDTO> Handle(GetByCodigoRecetaQuery request, CancellationToken cancellationToken)
        {
            var formulaMedica = await _formulaMedicaServices.GetByCodigoRecetaAsync(request._codigoReceta);
            return _mapper.Map<FormulaMedicaDTO>(formulaMedica);
        }
    }
}