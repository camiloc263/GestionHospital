using CitasMedicas.Application.Commands;
using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Queries;
using CitasMedicas.Application.Services;
using CitasMedicas.Infrastructure.Messaging;
using MediatR;
using RecetasMedicas.Application.Commands;
using RecetasMedicas.Application.DTO;
using RecetasMedicas.Application.Queries;
using RecetasMedicas.Application.Services;
using RecetasMedicas.Domain.Entities;
using RecetasMedicas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace RecetasMedicas.Infrastructure.Controllers
{
    [RoutePrefix("api/formulamedica")]
    public class FormulaMedicaController : ApiController
    {
        private readonly IFormulaMedicaRepository _formulaMedicaRepository;
        private readonly PersonaClient _personaClient;
        private readonly IMediator _mediator;

        public FormulaMedicaController(IFormulaMedicaRepository formulaMedicaRepository, PersonaClient personaClient, IMediator mediator)
        {
            _formulaMedicaRepository = formulaMedicaRepository;
            _personaClient = personaClient;
            _mediator = mediator;
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAll()
        {

            var citas = await _mediator.Send(new GetAllFomulaQuery());
            return Ok(citas);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var formulaMedica = await _mediator.Send(new GetByIdQuery(id));
            if (formulaMedica == null)
            {
                return NotFound();
            }
            return Ok(formulaMedica);
        }

        [HttpGet]
        [Route("codigo/{codigoReceta}")]
        public async Task<IHttpActionResult> GetByCodigoReceta(string codigoReceta)
        {
            if (string.IsNullOrWhiteSpace(codigoReceta))
            {
                return BadRequest("El código de receta no puede estar vacío.");
            }

            var receta = await _formulaMedicaRepository.GetByCodigoRecetaAsync(codigoReceta);
            if (receta == null)
            {
                return NotFound();
            }

            return Ok(receta);
        }


        [HttpPut]
        [Route("{codigoReceta}")]
        public async Task<IHttpActionResult> UpdateByCodigoReceta(string codigoReceta, [FromBody] FormulaMedicaDTO updatedFormula)
        {
            var command = new UpdateFormulaCommand(codigoReceta, updatedFormula);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return Ok(result);

        }
    
           [HttpPost]
           [Route("agregar")]
           public async Task<IHttpActionResult> AddFormulaMedica([FromBody] FormulaMedicaDTO formulaMedica)
           {
            var command = new AddFormulaCommand(formulaMedica);
            var id = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigoReceta}")]
        public async Task<IHttpActionResult> DeleteByCodigoReceta(string codigoReceta)
        {
            var result = await _mediator.Send(new DeleteFormulaCommand(codigoReceta));
            if (!result)
                return NotFound();

            return Ok();
        }

      }
}