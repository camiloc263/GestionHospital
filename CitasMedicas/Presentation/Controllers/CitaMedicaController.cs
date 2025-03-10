using CitasMedicas.Application.Commands;
using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Queries;
using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Entities;
using CitasMedicas.Infrastructure.Repository;
using MediatR;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CitasMedicas.Infrastructure.Controllers
{
    [RoutePrefix("api/citaMedica")]
    public class CitaMedicaController : ApiController
    {
        private readonly ICitaMedicaServices citaMedicaServices;
        private readonly PersonaClient personaClient;
        private readonly IMediator _mediator;


        public CitaMedicaController(ICitaMedicaServices citaMedicaServices, PersonaClient personaClient,IMediator mediator)
        {
            this.citaMedicaServices = citaMedicaServices;
            this.personaClient = personaClient;
            _mediator = mediator;

        }
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAll()
        {
            var citas = await _mediator.Send(new GetAllCitasQuery());
            return Ok(citas);
        }
        [HttpGet]
        [Route("getBy/{Id}")]
        public async Task<IHttpActionResult> GetCitasByIdQuery(int Id)
        {
            var result = await _mediator.Send(new GetCitasByIdQuery(Id));
            return Ok(result);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IHttpActionResult> UpdateCita(int Id,[FromBody] CitaDto citaDto)
        {
            var command = new UpdateCitaCommand(Id, citaDto);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return Ok(result);

        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> AddCita([FromBody] CitaDto cita)
        {
            var command = new AddCitaCommand(cita);
            var id = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{idcita}")]
        public async Task<IHttpActionResult> DeleteCita(int idcita)
        {
            var result = await _mediator.Send(new DeleteCitaCommand(idcita));
            if (!result)
              return NotFound();

            return Ok();
        }
       
           [HttpGet]
           [Route("{fecha}")]
           public async Task<IHttpActionResult> GetByDate(string fecha)
           {
               DateTime.TryParse(fecha, out DateTime fechaParsed);

               var citas = await citaMedicaServices.GetByDate(fechaParsed);

               if (citas == null || citas.Count == 0)
               {
                   return NotFound();
               }

               return Ok(citas);
           }

           [HttpPut]
           [Route("{idPaciente}")]
           public async Task<IHttpActionResult> UpdateCitaByPacienteId(int idPaciente, [FromBody] CitaMedica cita)
           {


               var resultado = await citaMedicaServices.UpdateCitaByPacienteId(idPaciente, cita);

               if (!resultado)
                   return NotFound();

               return Ok(resultado);
           }

           [HttpGet]
           [Route("{numeroDocumento}")]
           public async Task<IHttpActionResult> GetPersonaInfo(string numeroDocumento)
           {
               var persona = await personaClient.GetPersonaByDocumento(numeroDocumento);
               if (persona == null)
                   return NotFound();

               return Ok(persona);
           }
           [HttpPut]
           [Route("finalizar/{idUsuario}")]
           public async Task<IHttpActionResult> FinalizarCitaAsync(int idUsuario, [FromBody] CitaMedica cita)
           {
               var resultado = await citaMedicaServices.FinalizarCitaAsync(idUsuario, cita);

               if (!resultado)
                   return NotFound();

               return Ok("Cita finalizada exitosamente");
           }

    }
}
