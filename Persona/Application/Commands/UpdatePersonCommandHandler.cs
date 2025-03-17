using AutoMapper;
using MediatR;
using Persona.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Persona.Application.Services;

namespace Persona.Application.Commands
{
	public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonaServices _personaService;

        public UpdatePersonCommandHandler(IPersonaServices personaService, IMapper mapper)
        {
          _personaService = personaService;
        }
        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            // Obtener la persona mediante algún método asíncrono (por ejemplo, GetByDocumentoAsync)
            var personaEntitie = await _personaService.GetByDocumentoAsync(request.Identificacion);
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
            await _personaService.UpdatePerson(personaEntitie);
            return true;
        }
    }
}