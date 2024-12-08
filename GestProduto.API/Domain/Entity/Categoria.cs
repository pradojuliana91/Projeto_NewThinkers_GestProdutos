namespace GestProduto.API.Domain.Entity
{
    public class Categoria
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}
