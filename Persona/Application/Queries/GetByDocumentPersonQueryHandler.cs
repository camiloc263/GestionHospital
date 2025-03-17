using AutoMapper;
using MediatR;
using Persona.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Persona.Application.DTO;
using Persona.Application.Services;

namespace Persona.Application.Queries
{
	public class GetByDocumentPersonQueryHandler : IRequestHandler<GetByDocumentPersonQuery, PersonasDto>
    {
        private readonly IPersonaServices _personaService;
        private readonly IMapper _mapper;

        public GetByDocumentPersonQueryHandler(IPersonaServices personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        public async Task<PersonasDto> Handle(GetByDocumentPersonQuery request, CancellationToken cancellationToken)
        {
            var persona = await _personaService.GetByDocumentoAsync(request._Identificacion);
            return _mapper.Map<PersonasDto>(persona);
        }
    }
}