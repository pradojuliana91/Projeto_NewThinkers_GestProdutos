namespace GestProduto.API.Controllers.Request
{
    public class ProdutoRequest
    {
        public string? Nome { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorVenda { get; set; }
        public int? CategoriaId { get; set; }
    }
}
