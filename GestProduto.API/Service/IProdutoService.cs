using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;

namespace GestProduto.API.Service
{
    public interface IProdutoService
    {
        List<CategoriaResponse> ListarCategoriasDeProduto();
        ProdutoResponse BuscaPorId(int id);
        List<ProdutoResponse> BuscarProdutos(int? id, string? nome, int? categoriaId, bool? disponivel);
        ProdutoCadastroResponse Cadastrar(ProdutoRequest produtoRequest);
        void Atualizar(int id, ProdutoRequest produtoRequest);
        void AtualizarEstoque(int id, int? quantidade);
        void Deletar(int id);
    }
}
