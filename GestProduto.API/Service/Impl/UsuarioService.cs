using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;
using GestProduto.API.Domain.Entity;
using GestProduto.API.Domain.Repository;
using GestProduto.API.Exceptions;

namespace GestProduto.API.Service.Impl
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITokenService tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            this.usuarioRepository = usuarioRepository;
            this.tokenService = tokenService;
        }

        public TokenResponse Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Login) || string.IsNullOrEmpty(loginRequest.Senha))
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Usuario? usuario = usuarioRepository.Login(loginRequest.Login, loginRequest.Senha);

            if (usuario == null)
            {
                throw new UnauthorizedException("Usuário ou senha inválido.");
            }

            return tokenService.GeraToken(usuario.NomeUsuario, usuario.Perfil);
        }
    }
}
