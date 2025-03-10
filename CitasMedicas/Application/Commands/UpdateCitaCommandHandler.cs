using AutoMapper;
using CitasMedicas.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMedicas.Domain.Interfaces;
using System.Web.Http.Results;

namespace CitasMedicas.Application.Commands
{
	public class UpdateCitaCommandHandler: IRequestHandler<UpdateCitaCommand, bool >
	{
        private readonly ICitaMedicaRepository _citaRepository;
        public UpdateCitaCommandHandler(ICitaMedicaRepository citaRepository, IMapper mapper)
        {
            _citaRepository = citaRepository;
       
        }
        public async Task<bool> Handle(UpdateCitaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var citaEntitie = await _citaRepository.GetById(request.Id);
                if (citaEntitie == null)
                {
                    return false;
                }

                // Actualizar propiedades
                citaEntitie.idpaciente = request._citaDto.idpaciente;
                citaEntitie.idmedico = request._citaDto.idmedico;
                citaEntitie.lugarcita = request._citaDto.lugarcita;
                citaEntitie.fechacita = request._citaDto.fechacita;
                citaEntitie.estadocita = request._citaDto.estadocita;

                // Actualizar la persona en el repositorio
                await _citaRepository.Update(citaEntitie);

                return true;
            }
            catch (Exception ex)
            {
                // Agregar logging para depuración
                Console.WriteLine($"Error en UpdateCitaCommandHandler: {ex.Message}");
                return false;
            }
        }
        }

    }
