using CitasMedicas.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMedicas.Application.Services
{
   public interface ICitaMedicaServices
    {
        Task<List<CitaMedica>> GetAll();
        Task<CitaMedica> GetById(int id);
        Task<bool> Update(CitaMedica cita);
        Task<bool> UpdateCitaByPacienteId(int idPaciente, CitaMedica cita);
        Task<bool> AddCita(CitaMedica cita);
        Task<bool> DeleteCita(int idcita);
        Task<bool> FinalizarCitaAsync(int iduausuario, CitaMedica cita);
        Task<List<CitaMedica>> GetByDate(DateTime fecha);



    }
}
