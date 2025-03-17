using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Services;
using System.Web.Http;



namespace CitasMedicas.Presentation.Controllers
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