using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Core.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Infrastructure.Shared
{
    public class JWTService : IJWTService
    {

        public JWTSettings _settings { get; }

        public JWTService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateJWToken(Personal personal)
        {
            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(sc);

            var claims = new[] {
                new Claim(ClaimTypes.Name , personal.Apellido),
                new Claim(ClaimTypes.Email , personal.Usuario),
                new Claim(ClaimTypes.DateOfBirth , personal.FechaNacimiento.ToString()),
                //new Claim(ClaimTypes.Role , personal.IdRol== "A" ? "Admin" : "User"),
                new Claim("UserId" , personal.IdPersonal.ToString()),
            };

            var payload = new JwtPayload(
                            _settings.Issuer,
                            _settings.Audience,
                            claims,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes));
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}



