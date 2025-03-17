using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Owin.Security;

namespace CitasMedicas.App_Start
{
	public class Startup
	{
        public void Configuration(IAppBuilder app)
        {

            {
                app.UseJwtBearerAuthentication(
                    new JwtBearerAuthenticationOptions
                    {
                        AuthenticationMode = AuthenticationMode.Active,
                        TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = ConfigurationManager.AppSettings["JwtIssuer"],
                            ValidAudience = ConfigurationManager.AppSettings["JwtIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JwtKey"]))
                        }
                    });
            }
        }
    }
}