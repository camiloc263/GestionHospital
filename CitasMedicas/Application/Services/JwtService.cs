using CitasMedicas.Application.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CitasMedicas.Application.Services
{
	public class JwtService
	{
        public async Task<string> GetToken(UserDto loginDto)
        {
            var key = ConfigurationManager.AppSettings["JwtKey"];
            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer))
            {
                throw new InvalidOperationException("JWT configuration is missing.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", loginDto.id) // Ahora usa el valor real de userId
            };

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                permClaims,
                expires: DateTime.UtcNow.AddMinutes(2), // Usar UTC
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(jwtToken);
        }

    }
}