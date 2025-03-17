using MediatR;
using Persona.Application.Services;
using System.Threading;
using System.Threading.Tasks;


namespace Persona.Application.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonaServices _personaService;

        public DeletePersonCommandHandler(IPersonaServices personaService)
        {
            _personaService = personaService;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var persona = await _personaService.GetByDocumentoAsync(request._identificacion);
            if (persona == null)
            {
                return false; // No se encontró la persona
            }

            return await _personaService.DeleteAsync(persona);
        }
    }
}