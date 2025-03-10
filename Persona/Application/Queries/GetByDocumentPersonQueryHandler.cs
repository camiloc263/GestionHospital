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

namespace Persona.Application.Queries
{
	public class GetByDocumentPersonQueryHandler : IRequestHandler<GetByDocumentPersonQuery, PersonasDto>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public GetByDocumentPersonQueryHandler(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<PersonasDto> Handle(GetByDocumentPersonQuery request, CancellationToken cancellationToken)
        {
            var persona = await _personaRepository.GetByDocumentoAsync(request._Identificacion);
            return _mapper.Map<PersonasDto>(persona);
        }
    }
}