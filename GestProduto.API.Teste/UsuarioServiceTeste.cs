using Moq;
using GestProduto.API.Domain.Repository;
using GestProduto.API.Exceptions;
using GestProduto.API.Controllers.Request;
using GestProduto.API.Service.Impl;
using GestProduto.API.Service;
using GestProduto.API.Domain.Entity;
using GestProduto.API.Controllers.Response;

namespace GestProduto.API.Teste
{
    public class UsuarioServiceTeste
    {
        [Fact]
        public void Retorna_Erro_Bad_Request_Login_Dados_Invalidos()
        {
            //Arrange
            Mock<IUsuarioRepository> usuarioRepository = new Mock<IUsuarioRepository>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();

            LoginRequest loginRequest = new LoginRequest
            {
                Login = null,
                Senha = null
            };

            UsuarioService usuarioService = new UsuarioService(usuarioRepository.Object, tokenService.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => usuarioService.Login(loginRequest));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Unauthorized_Login_Invalido()
        {
            //Arrange
            Mock<IUsuarioRepository> usuarioRepository = new Mock<IUsuarioRepository>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();          

            LoginRequest loginRequest = new LoginRequest
            {
                Login = "user1",
                Senha = "senha1"
            };

            usuarioRepository.Setup(repo => repo.Login(loginRequest.Login, loginRequest.Senha)).Returns(value: null);

            UsuarioService usuarioService = new UsuarioService(usuarioRepository.Object, tokenService.Object);

            //Act
            var myException = Assert.Throws<UnauthorizedException>(() => usuarioService.Login(loginRequest));

            //Assert
            Assert.Equal("Usuário ou senha inválido.", myException.Message);
        }

        [Fact]
        public void Retorna_Login()
        {
            //Arrange
            Mock<IUsuarioRepository> usuarioRepository = new Mock<IUsuarioRepository>();
            Mock<ITokenService> tokenService = new Mock<ITokenService>();

            LoginRequest loginRequest = new LoginRequest
            {
                Login = "user1",
                Senha = "senha1"
            };

            Usuario usuario = new Usuario()
            {
                Id = 1,
                Email = "user1@teste.com",
                Nome = "user",
                NomeUsuario = "user1",
                Perfil = PerfilUsuarioEnum.ADMIN,
                Senha = "xxxxxxxxxx"
            };

            usuarioRepository.Setup(repo => repo.Login(loginRequest.Login, loginRequest.Senha)).Returns(value: usuario);

            TokenResponse tokenResponse = new TokenResponse()
            {
                Token = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                ValidoAte = DateTime.Now.AddMinutes(15)
            };

            tokenService.Setup(service => service.GeraToken(usuario.NomeUsuario, usuario.Perfil)).Returns(value: tokenResponse);

            UsuarioService usuarioService = new UsuarioService(usuarioRepository.Object, tokenService.Object);

            //Act
            var response = usuarioService.Login(loginRequest);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(tokenResponse.Token, response.Token);
            Assert.Equal(tokenResponse.ValidoAte, response.ValidoAte);
        }
    }
}