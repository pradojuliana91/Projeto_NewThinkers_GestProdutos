namespace GestProduto.API.Domain.Entity
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int CategoriaId { get; set; }        
        public int Quantidade { get; set; }
        public decimal ValorVenda { get; set; }
        public required Categoria Categoria { get; set; }
    }
}
