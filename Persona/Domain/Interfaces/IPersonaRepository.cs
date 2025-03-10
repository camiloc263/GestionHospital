using Persona.Application.DTO;
using Persona.Domain.Entities;
using Persona.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persona.Domain.Interface
{
   public interface IPersonaRepository
    {
        Task <List<ListaPersona>> GetAll();
        Task<ListaPersona> GetByDocumentoAsync(string Identificacion);
        Task<bool> UpdatePerson(ListaPersona persona);
        Task<bool> AddAsync(ListaPersona persona);
        Task<bool> DeleteAsync(ListaPersona persona);


    }
}
