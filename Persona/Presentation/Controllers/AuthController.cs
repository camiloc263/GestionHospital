using Persona.Application.DTO;
using Persona.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Persona.Presentation.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController: ApiController
	{
        private readonly JwtService _recetasService;

        public AuthController(JwtService recetasService)
        {
            _recetasService = recetasService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route()]
        public IHttpActionResult Login(UserDto loginDto)
        {
            var token = _recetasService.GetToken(loginDto);
            return Ok(new { Token = token });
        }
    }
}
