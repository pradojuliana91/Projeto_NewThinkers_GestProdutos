namespace GestProduto.API.Controllers.Response
{
    public class ProdutoResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required bool Disponivel { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorVenda { get; set; }
        public required CategoriaResponse Categoria { get; set; }
    }
}
