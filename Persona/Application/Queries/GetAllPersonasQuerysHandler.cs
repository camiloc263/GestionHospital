using MediatR;
using Persona.Domain.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Persona.Application.DTO;
using Persona.Application.Queries;
using Persona.Domain.Entities;
using AutoMapper;
using Persona.Application.Services;

namespace Persona.Application.Commands
{
    public class GetAllPersonasQuerysHandler : IRequestHandler<GetAllPersonasQuery, List<PersonasDto>>
    {
        private readonly IPersonaServices _personaService;
        private readonly IMapper _mapper;

        public GetAllPersonasQuerysHandler(IPersonaServices personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        public async Task<List<PersonasDto>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken)
        {
            var personas = await _personaService.GetAll(); 
            return _mapper.Map<List<PersonasDto>>(personas);
        }

      }
}