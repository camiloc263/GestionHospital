using CitasMedicas.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMedicas.Domain.Interfaces
{
    public interface ICitaMedicaRepository
    {
        Task<List<CitaMedica>> GetAll();
        Task<CitaMedica> GetById(int id);
        Task<bool> Update(CitaMedica cita);
        Task<bool> UpdateCitaByPacienteId(int idPaciente, CitaMedica cita);
        Task<bool> AddAsync(CitaMedica cita);
        Task<bool> DeleteAsync(CitaMedica cita);
        //Task<bool> ValidarExistenciaPersona(int idpaciente);
        Task<bool> FinalizarCitaAsync(int iduausuario, CitaMedica cita);
        Task<List<CitaMedica>> GetByDate(DateTime fecha);

       
    }
}
