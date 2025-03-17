using RecetasMedicas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecetasMedicas.Domain.Interfaces
{
    public interface IFormulaMedicaRepository
    {
        Task<List<FormulaMedica>> GetAll();
        Task<FormulaMedica> GetById(int id);
        Task<FormulaMedica> GetByCodigoRecetaAsync(string codigoReceta);
        Task<bool> UpdateAsync(FormulaMedica formulaMedica);
        Task<bool> AddFormulaMedicaAsync(FormulaMedica formulaMedica);
        Task<bool> DeleteByCodigoRecetaAsync(FormulaMedica formulaMedica);
    }
}