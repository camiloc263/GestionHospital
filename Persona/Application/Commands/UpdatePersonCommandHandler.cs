using AutoMapper;
using MediatR;
using Persona.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace Persona.Application.Commands
{
	public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;


        public UpdatePersonCommandHandler(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;

        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            // Obtener la persona mediante algún método asíncrono (por ejemplo, GetByDocumentoAsync)
            var personaEntitie = await _personaRepository.GetByDocumentoAsync(request.Identificacion);
            if (personaEntitie == null)
            {
                return false;
            }

            // Actualizar propiedades
            personaEntitie.tipoDocumento = request._personaDto.tipoDocumento;
            personaEntitie.numeroDocumento = request._personaDto.numeroDocumento;            
            personaEntitie.nombre = request._personaDto.nombre;
            personaEntitie.apellidoUno = request._personaDto.apellidoUno;
            personaEntitie.apellidoDos = request._personaDto.apellidoDos;
            personaEntitie.direccion = request._personaDto.direccion;
            personaEntitie.telefono = request._personaDto.telefono;
            personaEntitie.correo = request._personaDto.correo;
            personaEntitie.fechaNacimiento = request._personaDto.fechaNacimiento;
            personaEntitie.tipoUsuario = request._personaDto.tipoUsuario;

            // Actualizar la persona en el repositorio
            await _personaRepository.UpdatePerson(personaEntitie);
            return true;
        }
    }
}