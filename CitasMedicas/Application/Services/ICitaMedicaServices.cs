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
        Task<bool> AddAsync(CitaMedica cita);
        Task<bool> DeleteAsync(CitaMedica cita);
        Task<bool> FinalizarCitaAsync(int iduausuario, CitaMedica cita);
        
    }
}
