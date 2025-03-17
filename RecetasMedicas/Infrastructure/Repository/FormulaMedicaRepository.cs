using CitasMedicas.Application.Services;
using RecetasMedicas.Application.Services;
using RecetasMedicas.Domain.Entities;
using RecetasMedicas.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;


namespace RecetasMedicas.Infrastructure.Repository
{
    public class FormulaMedicaRepository : IFormulaMedicaRepository
    {
        private readonly FormulaMedicaContext context;

        public FormulaMedicaRepository(FormulaMedicaContext context)
        {
            this.context = context;
        }

        public async Task<List<FormulaMedica>> GetAll()
        {
            return await context.RecetaMedica.ToListAsync();
        }

        public async Task<FormulaMedica> GetById(int id)
        {
            return await context.RecetaMedica.FindAsync(id);
        }
        public async Task Update(FormulaMedica formulaMedica)
        {
            context.Entry(formulaMedica).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task<FormulaMedica> GetByCodigoRecetaAsync(string codigoReceta)
        {
            return await context.RecetaMedica
                .FirstOrDefaultAsync(r => r.CodigoReceta == codigoReceta);
        }
        public async Task<bool> UpdateAsync(FormulaMedica formulaMedica)
        {
            context.Entry(formulaMedica).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddFormulaMedicaAsync(FormulaMedica formulaMedica)
        {

            context.RecetaMedica.Add(formulaMedica);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteByCodigoRecetaAsync(FormulaMedica formulaMedica)
        {
            context.RecetaMedica.Remove(formulaMedica);
            await context.SaveChangesAsync();
            return true;

        }

    }
}
