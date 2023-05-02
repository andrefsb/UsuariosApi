using System.Security.Claims;
using UsuariosApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Reflection.Metadata.Ecma335;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.Birthday.ToString())
            };

            var key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("myConfiguration")["Chave"];

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.ToString()));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
