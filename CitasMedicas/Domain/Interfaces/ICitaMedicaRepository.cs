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
        Task<bool> AddCita(CitaMedica cita);
        Task<bool> DeleteCita(CitaMedica idcita);
        Task<bool> FinalizarCitaAsync(int iduausuario, CitaMedica cita);
        


    }
}
