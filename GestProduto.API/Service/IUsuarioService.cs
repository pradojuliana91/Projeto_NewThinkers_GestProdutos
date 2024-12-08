using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;

namespace GestProduto.API.Service
{
    public interface IUsuarioService
    {
        TokenResponse Login(LoginRequest loginRequest);
    }
}