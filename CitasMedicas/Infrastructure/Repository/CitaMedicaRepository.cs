using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Infrastructure.Repository
{
    public class CitaMedicaRepository : ICitaMedicaRepository
    {
        private readonly IRabitMqRepository _rabbitMQProduce;
        private readonly CitaMedicaContext _context;

        public CitaMedicaRepository(CitaMedicaContext context, IRabitMqRepository rabbitMQProduce)
        {
            _context = context;
            _rabbitMQProduce = rabbitMQProduce;
        }
        public async Task<List<CitaMedica>> GetAll()
        {
            return await _context.citamedica.ToListAsync();
        }

        public async Task<CitaMedica> GetById(int id)
        {
            return await _context.citamedica.FirstOrDefaultAsync(c => c.idcita == id);
        }
        public async Task<bool> Update(CitaMedica cita)
        {
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<CitaMedica> GetByPersonaId(int idpaciente)
        {
            return await _context.citamedica.FirstOrDefaultAsync(c => c.idpaciente == idpaciente);
        }

        public async Task<bool> AddCita(CitaMedica cita)
        {

            _context.citamedica.Add(cita);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteCita(CitaMedica cita)
        {

            _context.citamedica.Remove(cita);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidarExistenciaPersona(int idpaciente)
        {
            return await _context.citamedica.AnyAsync(c => c.idpaciente == idpaciente);
        }

        public async Task<bool> FinalizarCitaAsync(int idPaciente, CitaMedica cita)
        {
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;


        }


    }
}