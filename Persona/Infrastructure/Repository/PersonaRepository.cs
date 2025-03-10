using Persona.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persona.Application.DTO;
using System.Linq;
using Persona.Domain.Entities;
using System.Data.Entity;
using System.Web.Http.Results;

namespace Persona.Infrastructure.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaContext _context;

        public PersonaRepository(PersonaContext context)
        {
            _context = context;
        }
        public async Task<List<ListaPersona>> GetAll()
        {
            return await _context.ListaPersona.ToListAsync();
        }

        public async Task<ListaPersona> GetByDocumentoAsync(string Identificacion)
        {
            return await _context.ListaPersona.FirstOrDefaultAsync(p => p.numeroDocumento == Identificacion);
        }
        public async Task<bool> UpdatePerson(ListaPersona persona)
        {

            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> AddAsync(ListaPersona persona)
        {
            _context.ListaPersona.Add(persona);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(ListaPersona persona)
        {
            _context.ListaPersona.Remove(persona);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
