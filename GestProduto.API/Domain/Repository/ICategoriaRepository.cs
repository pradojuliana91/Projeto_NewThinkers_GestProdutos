using GestProduto.API.Domain.Entity;

namespace GestProduto.API.Domain.Repository
{
    public interface ICategoriaRepository
    {
        List<Categoria>? Listar();
        Categoria? BuscaPorId(int id);
    }
}