namespace GestProduto.API.Controllers.Response
{
    public class TokenResponse
    {
        public required string Token { get; set; }
        public required DateTime ValidoAte {  get; set; }
    }
}
