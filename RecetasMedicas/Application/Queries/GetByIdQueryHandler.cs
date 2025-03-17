using AutoMapper;
using MediatR;
using RecetasMedicas.Application.DTO;
using RecetasMedicas.Application.Services;
using RecetasMedicas.Domain.Entities;
using RecetasMedicas.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RecetasMedicas.Application.Queries
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, FormulaMedicaDTO>
    {

        private readonly IFormulaMedicaServices _formulaMedicaServices;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IFormulaMedicaServices formulaMedicaServices, IMapper mapper)
        {
            _formulaMedicaServices = formulaMedicaServices;
            _mapper = mapper;
        }

        public async Task<FormulaMedicaDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var formulaMedica = await _formulaMedicaServices.GetById(request._Id);
            return _mapper.Map<FormulaMedicaDTO>(formulaMedica);
        }
    }
}