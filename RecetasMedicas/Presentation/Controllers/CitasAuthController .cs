using CitasMedicas.Application.DTO;
using RecetasMedicas.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RecetasMedicas.Presentation.Controllers
{
    [RoutePrefix("api/recetas/auth")]
    public class CitasAuthController : ApiController
    {
        private readonly JwtServiceR _recetasService;

        public CitasAuthController(JwtServiceR recetasService)
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