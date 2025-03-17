using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Owin;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(Persona.App_Start.Startup))]
namespace Persona.App_Start
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
    

