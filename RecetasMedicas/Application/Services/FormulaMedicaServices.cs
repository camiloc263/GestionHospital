using RecetasMedicas.Domain.Entities;
using RecetasMedicas.Domain.Interfaces;
using RecetasMedicas.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RecetasMedicas.Application.Services
{
    public class FormulaMedicaServices : IFormulaMedicaServices
    {
        private readonly IFormulaMedicaRepository _formulaMedicaRepository;

        public FormulaMedicaServices(IFormulaMedicaRepository formulaMedicaRepository)
        {
            _formulaMedicaRepository = formulaMedicaRepository;
        }
        public async Task<List<FormulaMedica>> GetAllFormulasMedicas()
        {
            return await _formulaMedicaRepository.GetAll();
        }
        public async Task<FormulaMedica> GetById(int id)
        {
            return await _formulaMedicaRepository.GetById(id);
        }
        public async Task<FormulaMedica> GetByCodigoRecetaAsync(string codigoReceta)
        {
            return await _formulaMedicaRepository.GetByCodigoRecetaAsync(codigoReceta);
        }

        public async Task<bool> UpdateAsync(FormulaMedica formulaMedica)
        {
            await _formulaMedicaRepository.UpdateAsync(formulaMedica);
            return true;

        }
        public async Task<bool> UpdateByCodigoRecetaAsync(string codigoReceta, FormulaMedica updatedFormula)
        {
            var existingFormula = await _formulaMedicaRepository.GetByCodigoRecetaAsync(codigoReceta);
            if (existingFormula == null)
            {
                return false;
            }

            existingFormula.FechaEmision = updatedFormula.FechaEmision;
            existingFormula.FechaVencimiento = updatedFormula.FechaVencimiento;
            existingFormula.Estado = updatedFormula.Estado;
            existingFormula.IdPaciente = updatedFormula.IdPaciente;

            await _formulaMedicaRepository.UpdateAsync(existingFormula);
            return true;
        }
        public async Task<bool> AddFormulaMedicaAsync(FormulaMedica formulaMedica)
        {
            return await _formulaMedicaRepository.AddFormulaMedicaAsync(formulaMedica);
        }
        public async Task<bool> DeleteByCodigoRecetaAsync(FormulaMedica formulaMedica)
        {
            return await _formulaMedicaRepository.DeleteByCodigoRecetaAsync(formulaMedica);
        }
        public async Task CrearRecetaAsync(FormulaMedica receta)
        {
            await _formulaMedicaRepository.AddFormulaMedicaAsync(receta);
        }

    }
}