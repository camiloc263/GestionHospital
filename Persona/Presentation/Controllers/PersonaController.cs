using MediatR;
using Persona.Application.Commands;
using Persona.Application.DTO;
using Persona.Application.Queries;
using System.Threading.Tasks;
using System.Web.Http;

namespace Persona.Infrastructure.Controllers
{
    [Authorize]
    [RoutePrefix("api/persona")]
    public class PersonaController : ApiController
    {
        private readonly IMediator _mediator;
      

        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAll()
        {
            var personas = await _mediator.Send(new GetAllPersonasQuery());
            return Ok(personas);
        }
        [HttpGet]
        [Route("{Identificacion}")]
        public async Task<IHttpActionResult> GetByDocumentPersonQuery(string Identificacion)
        {
            var result = await _mediator.Send(new GetByDocumentPersonQuery(Identificacion));
            return Ok(result);
        }

        [HttpPut]
        [Route("{Identificacion}")]
        public async Task<IHttpActionResult> UpdatePerson(string identificacion, [FromBody] PersonasDto personaDto)
        {
            var command = new UpdatePersonCommand(identificacion, personaDto);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> addPerson([FromBody] PersonasDto personas)
        {
            var command = new AddPersonCommand(personas);
            var id = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{identificacion}")]
        public async Task<IHttpActionResult> DeletePerson(string identificacion)
        {
            var result = await _mediator.Send(new DeletePersonCommand(identificacion));
            if (!result)
                return NotFound();

            return Ok();
        }





    }
}
