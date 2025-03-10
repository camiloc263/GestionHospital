using MediatR;
using Persona.Domain.Interface;
using System.Threading.Tasks;
using System.Threading;


namespace Persona.Application.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;

        public DeletePersonCommandHandler(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var persona = await _personaRepository.GetByDocumentoAsync(request._identificacion);
            if (persona == null)
            {
                return false; // No se encontró la persona
            }

            return await _personaRepository.DeleteAsync(persona);
        }
    }
}