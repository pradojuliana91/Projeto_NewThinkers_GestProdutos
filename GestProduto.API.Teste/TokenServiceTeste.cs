using Moq;
using GestProduto.API.Domain.Entity;
using GestProduto.API.Domain.Repository;
using GestProduto.API.Exceptions;
using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;
using GestProduto.API.Service.Impl;
using GestProduto.API.Service;
using Microsoft.Extensions.Configuration;

namespace GestProduto.API.Teste
{
    public class TokenServiceTeste
    {
        [Fact]
        public void Retorna_Token()
        {
            //Arrange
            string key = "PWwSchVomVdNvym1jJLEyMUxQNWfzBcM";
            string login = "user1";
            PerfilUsuarioEnum perfil = PerfilUsuarioEnum.ADMIN;

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.SetupGet(x => x[It.Is<string>(s => s == "jwt-secret-key")]).Returns(key);

            TokenService token = new TokenService(config.Object);

            //Act
            var response = token.GeraToken(login, perfil);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.True(response.ValidoAte > DateTime.Now.AddMinutes(14));
            Assert.True(response.ValidoAte < DateTime.Now.AddMinutes(20));
        }
    }
}