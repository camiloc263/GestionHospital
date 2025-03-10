using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Messaging;
using CitasMedicas.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CitasMedicas.Infrastructure.Repository
{
	public class CitaMedicaRepository : ICitaMedicaRepository
	{
        private readonly ICitaMedicaServices citaMedicaServices;
        private readonly PersonaClient personaClient;
        private readonly RabbitMQProduce rabbitMQProducer;

        public CitaMedicaRepository(ICitaMedicaServices citaMedicaServices, PersonaClient personaClient, RabbitMQProduce rabbitMQProducer)
        {
            this.citaMedicaServices = citaMedicaServices;
            this.personaClient = personaClient;
            this.rabbitMQProducer = rabbitMQProducer;
        }
        public async Task<List<CitaMedica>> GetAll()
        {
            return await citaMedicaServices.GetAll();
        }

        public async Task<CitaMedica> GetById(int id)
        {
            return await citaMedicaServices.GetById(id);
        }
        public async Task<bool> Update(CitaMedica cita)
        {
            return await citaMedicaServices.Update(cita);
        }
        public async Task<bool> UpdateCitaByPacienteId(int idPaciente, CitaMedica cita)
         {
             return await citaMedicaServices.UpdateCitaByPacienteId(idPaciente, cita);
         }
         public async Task<bool> AddAsync(CitaMedica cita)
         {
               return await citaMedicaServices.AddCita(cita);
         }
         public async Task<bool> DeleteAsync(CitaMedica cita)
         {
             return await citaMedicaServices.DeleteCita(cita);
         }

         public async Task<CitaMedica> AgendarCita(string numeroDocumento, DateTime fecha)
         {

             var persona = await personaClient.GetPersonaByDocumento(numeroDocumento);


             var nuevaCita = new CitaMedica
             {
                 idpaciente = persona.idpaciente, 
                 lugarcita = "Consultorio 1", 
                 fechacita = fecha,
                 estadocita = "Pendiente"
             };


             await citaMedicaServices.AddCita(nuevaCita);
             return nuevaCita;
         }

         public async Task<bool> FinalizarCitaAsync(int idUsiario, CitaMedica cita)
         {
             return await citaMedicaServices.FinalizarCitaAsync(idUsiario, cita);
         }

          public async Task<List<CitaMedica>> GetByDate(DateTime fecha)
         {
             return await citaMedicaServices.GetByDate(fecha);
         }
    }
}