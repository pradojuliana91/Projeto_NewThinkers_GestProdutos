namespace GestProduto.API.Controllers.Response
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}
