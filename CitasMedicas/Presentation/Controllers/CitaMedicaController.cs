using CitasMedicas.Application.Commands;
using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Queries;
using CitasMedicas.Application.Services;
using CitasMedicas.Infrastructure.Messaging;
using MediatR;
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
        public CitaMedicaController(ICitaMedicaServices citaMedicaServices, PersonaClient personaClient, IMediator mediator)
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
        public async Task<IHttpActionResult> UpdateCita(int Id, [FromBody] CitaDto citaDto)
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
        public async Task<IHttpActionResult> FinalizarCitaAsync(int idUsuario, [FromBody] RecetasDto receta)
        {
            var result = await _mediator.Send(new FinalizarCitaCommand(idUsuario, receta));

            if (!result)
                return NotFound(); ;


            return Ok(result);
        }
    }

}

