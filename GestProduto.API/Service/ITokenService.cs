using GestProduto.API.Controllers.Response;
using GestProduto.API.Domain.Entity;

namespace GestProduto.API.Service
{
    public interface ITokenService
    {
        TokenResponse GeraToken(string nomeUsuario, PerfilUsuarioEnum perfil);
    }
}
