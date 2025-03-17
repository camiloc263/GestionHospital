using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Messaging;
using CitasMedicas.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CitasMedicas.Application.Services
{
	public class CitaMedicaServices: ICitaMedicaServices
	{
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly PersonaClient personaClient;


        public CitaMedicaServices(ICitaMedicaRepository citaMedicaRepository, PersonaClient personaClient)
        {
           _citaMedicaRepository= citaMedicaRepository;
           this.personaClient = personaClient;
        }
        public async Task<List<CitaMedica>> GetAll()
        {
            return await _citaMedicaRepository.GetAll();
        }
        public async Task<CitaMedica> GetById(int id)
        {
            return await _citaMedicaRepository.GetById(id);
        }
        public async Task<bool> Update(CitaMedica cita)
        {
            return await _citaMedicaRepository.Update(cita);
        }
       public async Task<bool> AddAsync(CitaMedica cita)
        {
         return await _citaMedicaRepository.AddCita(cita);
        }
        public async Task<bool> DeleteAsync(CitaMedica cita)
        {
            return await _citaMedicaRepository.DeleteCita(cita);
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


            await _citaMedicaRepository.AddCita(nuevaCita);
            return nuevaCita;
        }

        public async Task<bool> FinalizarCitaAsync(int idUsiario, CitaMedica cita)
        {
            return await _citaMedicaRepository.FinalizarCitaAsync(idUsiario, cita);
        }

       

    }
}