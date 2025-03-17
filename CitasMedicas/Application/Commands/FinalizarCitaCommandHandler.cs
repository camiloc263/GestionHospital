using CitasMedicas.Application.Services;
using CitasMedicas.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CitasMedicas.Application.Commands
{
    public class FinalizarCitaCommandHandler : IRequestHandler<FinalizarCitaCommand, bool>
    {
        private readonly ICitaMedicaServices _citasService;
        private readonly IRabitMqRepository _rabbitSend;

        public FinalizarCitaCommandHandler(ICitaMedicaServices citasService, IRabitMqRepository rabbitSend)
        {
            _citasService = citasService;
            _rabbitSend = rabbitSend;
        }

        public async Task<bool> Handle(FinalizarCitaCommand request, CancellationToken cancellationToken)
        {
            var cita = await _citasService.GetById(request.IdCita);
            if (cita == null || request.Receta == null)
            {
                return false;
            }

            // Cambiar estado de la cita
            cita.estadocita = "Finalizada";
            await _citasService.Update(cita);

            // Enviar receta a RabbitMQ
            try
            {
                await _rabbitSend.PublicarMensaje(request.Receta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[❌] Error al enviar receta: {ex.Message}");
                return false;
            }
        }
    }
}