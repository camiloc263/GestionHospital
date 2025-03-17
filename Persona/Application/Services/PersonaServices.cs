using AutoMapper;
using Persona.Application.DTO;
using Persona.Application.Services;
using Persona.Domain.Entities;
using Persona.Domain.Interface;
using Persona.Infrastructure.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Persona.Application.Services
{
	public class PersonaServices: IPersonaServices
    {

        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;
        public PersonaServices(IPersonaRepository personaRepository, IMapper mapper)
        {
          _personaRepository = personaRepository; 
          _mapper = mapper;
        }
        public async Task<List<ListaPersona>> GetAll()
        {
         return  _mapper.Map<List<ListaPersona>>(_personaRepository.GetAll());
        }
        public async Task<ListaPersona> GetByDocumentoAsync(string numeroDocumento)
        {
         return _mapper.Map<ListaPersona>(_personaRepository.GetByDocumentoAsync(numeroDocumento));
        }
        public async Task<bool> UpdatePerson(ListaPersona persona)
        {
            return await _personaRepository.UpdatePerson(persona);
        }
        public async Task<bool> AddAsync(ListaPersona nuevaPersona)
        {
            return await _personaRepository.AddAsync(nuevaPersona);
        }
        public async Task<bool> DeleteAsync(ListaPersona persona)
        {
            return await _personaRepository.DeleteAsync(persona);
        }

       

    }
}