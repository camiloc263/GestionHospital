using RecetasMedicas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace RecetasMedicas.Application.Services
{
    public interface IFormulaMedicaServices
    {
        Task<List<FormulaMedica>> GetAllFormulasMedicas();
        Task<FormulaMedica> GetById(int id);
        Task<FormulaMedica> GetByCodigoRecetaAsync(string codigoReceta);
        Task<bool> UpdateAsync(FormulaMedica formulaMedica);
        Task<bool> UpdateByCodigoRecetaAsync(string codigoReceta, FormulaMedica updatedFormula);
        Task<bool> AddFormulaMedicaAsync(FormulaMedica formulaMedica);
        Task<bool> DeleteByCodigoRecetaAsync(FormulaMedica formulaMedica);
    }
}
