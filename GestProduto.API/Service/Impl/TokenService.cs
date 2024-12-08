using GestProduto.API.Controllers.Response;
using GestProduto.API.Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestProduto.API.Service.Impl
{
    public class TokenService : ITokenService
    {
        private readonly string secretkey;

        public TokenService(IConfiguration configuration)
        {
            this.secretkey = configuration["jwt-secret-key"]!;
        }

        public TokenResponse GeraToken(string nomeUsuario, PerfilUsuarioEnum perfil)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim(ClaimTypes.Role, perfil.ToString()),
                new Claim(ClaimTypes.Email, "rafael.gomes@tqi.com.br"),
                new Claim(ClaimTypes.Country, "Brasil")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime validoAte = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: validoAte,
                signingCredentials: creds);

            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidoAte = validoAte
            };
        }
    }
}
