
using Persona.Application.DTO;
using Persona.Domain.Entities;
using Persona.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persona.Application.Services
{
   public  interface IPersonaServices
    {
        Task<List<ListaPersona>> GetAll();
        Task<ListaPersona> GetByDocumentoAsync(string numeroDocumento);
        Task<bool> UpdatePerson(ListaPersona persona);
        Task<bool> AddAsync(ListaPersona persona);
        Task<bool> DeleteAsync(ListaPersona persona);

    }
}
