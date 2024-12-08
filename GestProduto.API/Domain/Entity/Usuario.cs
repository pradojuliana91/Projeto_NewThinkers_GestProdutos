namespace GestProduto.API.Domain.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string NomeUsuario { get; set; }
        public string? Senha { get; set; }
        public PerfilUsuarioEnum Perfil { get; set; }
    }
}
