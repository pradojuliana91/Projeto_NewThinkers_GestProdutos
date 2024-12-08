using GestProduto.API.Domain.Entity;

namespace GestProduto.API.Domain.Repository
{
    public interface IUsuarioRepository
    {
        Usuario? Login(string nomeUsuario, string senha);
    }
}