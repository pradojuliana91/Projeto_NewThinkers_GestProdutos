using GestProduto.API.Domain.Entity;

namespace GestProduto.API.Domain.Repository
{
    public interface IProdutoRepository
    {
        Produto? BuscaPorId(int id);
        List<Produto>? BuscarProdutos(int? id, string? nome, int? categoriaId, bool? disponivel);
        int Salvar(Produto produto);
        void Atualizar(int id, Produto produto);
        void AtualizarEstoque(int id, int quantidade);
        void Excluir(int id);
    }
}
