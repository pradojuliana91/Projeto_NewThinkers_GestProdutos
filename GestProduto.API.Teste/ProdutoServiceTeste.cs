using Moq;
using GestProduto.API.Domain.Entity;
using GestProduto.API.Domain.Repository;
using GestProduto.API.Exceptions;
using GestProduto.API.Controllers.Request;
using GestProduto.API.Service.Impl;

namespace GestProduto.API.Teste
{
    public class ProdutoServiceTeste
    {
        [Fact]
        public void Retorna_Lista_Categorias_De_Produto()
        {
            //Arrange
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();

            List<Categoria> categorias = new List<Categoria>();
            categorias.Add(new Categoria
            {
                Id = 1,
                Nome = "Categoria 1",
                Descricao = "Categoria descricao 1"
            });
            categorias.Add(new Categoria
            {
                Id = 2,
                Nome = "Categoria 2",
                Descricao = "Categoria descricao 2"
            });

            categoriaRepositoryMock.Setup(repo => repo.Listar()).Returns(value : categorias);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var response = produtoService.ListarCategoriasDeProduto();

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
            Assert.Equal(categorias[0].Id, response[0].Id);
            Assert.Equal(categorias[0].Nome, response[0].Nome);
            Assert.Equal(categorias[0].Descricao, response[0].Descricao);
            Assert.Equal(categorias[1].Id, response[1].Id);
            Assert.Equal(categorias[1].Nome, response[1].Nome);
            Assert.Equal(categorias[1].Descricao, response[1].Descricao);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Lista_Categorias_De_Produto()
        {
            //Arrange
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();

            List<Categoria>? categorias = null;

            categoriaRepositoryMock.Setup(repo => repo.Listar()).Returns(value : categorias);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.ListarCategoriasDeProduto());

            //Assert
            Assert.Equal("Nenhuma categoria encontrada.", myException.Message);
        }

        [Fact]
        public void Retorna_Produto_Por_Id_Dispponivel()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Produto produto = new Produto
            {
                Id = 1,
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value : produto);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var response = produtoService.BuscaPorId(produtoId);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(produto.Id, response.Id);
            Assert.Equal(produto.Nome, response.Nome);
            Assert.Equal(produto.Quantidade, response.Quantidade);
            Assert.Equal(produto.ValorVenda, response.ValorVenda);
            Assert.True(response.Disponivel);
            Assert.NotNull(produto.Categoria);
            Assert.Equal(produto.Categoria.Id, response.Categoria.Id);
            Assert.Equal(produto.Categoria.Nome, response.Categoria.Nome);
            Assert.Equal(produto.Categoria.Descricao, response.Categoria.Descricao);
        }

        [Fact]
        public void Retorna_Produto_Por_Id_Indispponivel()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Produto produto = new Produto
            {
                Id = 1,
                Nome = "Produto 1",
                Quantidade = 0,
                ValorVenda = 11.99m,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: produto);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var response = produtoService.BuscaPorId(produtoId);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(produto.Id, response.Id);
            Assert.Equal(produto.Nome, response.Nome);
            Assert.Equal(produto.Quantidade, response.Quantidade);
            Assert.Equal(produto.ValorVenda, response.ValorVenda);
            Assert.False(response.Disponivel);
            Assert.NotNull(produto.Categoria);
            Assert.Equal(produto.Categoria.Id, response.Categoria.Id);
            Assert.Equal(produto.Categoria.Nome, response.Categoria.Nome);
            Assert.Equal(produto.Categoria.Descricao, response.Categoria.Descricao);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Por_Id()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            int produtoId = 1;
            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: null);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.BuscaPorId(produtoId));

            //Assert
            Assert.Equal("Produto não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Busca_Produtos()
        {
            //Arrange
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            
            List<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto
            {
                Id = 1,
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            });
            produtos.Add(new Produto
            {
                Id = 2,
                Nome = "Produto 2",
                Quantidade = 0,
                ValorVenda = 20.00m,
                Categoria = new Categoria
                {
                    Id = 2,
                    Nome = "Categoria 2",
                    Descricao = "Categoria descricao 2"
                }
            });

            produtoRepositoryMock.Setup(repo => repo.BuscarProdutos(null, null, null, null)).Returns(value : produtos);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var response = produtoService.BuscarProdutos(null, null, null, null);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count());
            Assert.Equal(produtos[0].Id, response[0].Id);
            Assert.Equal(produtos[0].Nome, response[0].Nome);
            Assert.Equal(produtos[0].Quantidade, response[0].Quantidade);
            Assert.Equal(produtos[0].ValorVenda, response[0].ValorVenda);
            Assert.True(response[0].Disponivel);
            Assert.Equal(produtos[0].Categoria.Id, response[0].Categoria.Id);
            Assert.Equal(produtos[0].Categoria.Nome, response[0].Categoria.Nome);
            Assert.Equal(produtos[0].Categoria.Descricao, response[0].Categoria.Descricao);
            Assert.Equal(produtos[1].Id, response[1].Id);
            Assert.Equal(produtos[1].Nome, response[1].Nome);
            Assert.Equal(produtos[1].Quantidade, response[1].Quantidade);
            Assert.Equal(produtos[1].ValorVenda, response[1].ValorVenda);
            Assert.False(response[1].Disponivel);
            Assert.Equal(produtos[1].Categoria.Id, response[1].Categoria.Id);
            Assert.Equal(produtos[1].Categoria.Nome, response[1].Categoria.Nome);
            Assert.Equal(produtos[1].Categoria.Descricao, response[1].Categoria.Descricao);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Busca_Produtos()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            produtoRepositoryMock.Setup(repo => repo.BuscarProdutos(null, null, null, null)).Returns(value: null);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.BuscarProdutos(null, null, null, null));

            //Assert
            Assert.Equal("Nenhum produto encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Cadastro_Produto()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Categoria categorias = new Categoria
            {
                Id = 1,
                Nome = "Categoria 1",
                Descricao = "Categoria descricao 1"
            };

            categoriaRepositoryMock.Setup(repo => repo.BuscaPorId(categorias.Id)).Returns(value : categorias);


            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
                
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.Salvar(It.IsAny<Produto>())).Returns(value: produtoId);            
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var response = produtoService.Cadastrar(produto);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(produtoId, response.Id);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Cadastrar_Produto_Dados_Invalidos()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = null,
                Quantidade = null,
                ValorVenda = null,
                CategoriaId = null,
            };
            
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Cadastrar(produto));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Cadastrar_Produto_Categoria_Inexistente()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            categoriaRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value : null); 

            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
            };

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Cadastrar(produto));

            //Assert
            Assert.Equal("Categoria inválida.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Atualiza_Produto()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = null,
                Quantidade = null,
                ValorVenda = null,
                CategoriaId = null,

            };

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Cadastrar(produto));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Atualiza_Produto_Categoria_Inexistente()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            categoriaRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value : null);

            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
            };

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.Cadastrar(produto));

            //Assert
            Assert.Equal("Categoria inválida.", myException.Message);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Atualiza_Produto_Inexistente()
        {            
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Categoria categorias = new Categoria
            {
                Id = 1,
                Nome = "Categoria 1",
                Descricao = "Categoria descricao 1"
            };

            categoriaRepositoryMock.Setup(repo => repo.BuscaPorId(categorias.Id)).Returns(value: categorias);

            ProdutoRequest produto = new ProdutoRequest
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: null);

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            var myException = Assert.Throws<NotFoundException>(() => produtoService.Atualizar(produtoId, produto));

            //Assert
            Assert.Equal("Produto não encontrado.", myException.Message);
        }

        [Fact]
        public void Retorna_Atualiza_Produto()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Categoria categoria = new Categoria
            {
                Id = 1,
                Nome = "Categoria 1",
                Descricao = "Categoria descricao 1"
            };

            categoriaRepositoryMock.Setup(repo => repo.BuscaPorId(categoria.Id)).Returns(categoria);

            Produto produto = new Produto
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: produto);

            ProdutoRequest atualizaProdutoRequest = new ProdutoRequest
            {
                Nome = "Produto 1 XXXXXXXX",
                Quantidade = 20,
                ValorVenda = 50.00m,
                CategoriaId = 1
            };

            produtoRepositoryMock.Setup(repo => repo.Atualizar(produtoId, produto));
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

  
            //Act
            produtoService.Atualizar(produtoId, atualizaProdutoRequest);

            //Asser
            categoriaRepositoryMock.Verify(v => v.BuscaPorId(categoria.Id), Times.Once());
            produtoRepositoryMock.Verify(v => v.BuscaPorId(produtoId), Times.Once());
            produtoRepositoryMock.Verify(v => v.Atualizar(produtoId, produto), Times.Once());
        }

        [Fact]
        public void Retorna_Erro_Bad_Request_Atualiza_Estoque()
        {
            //Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            int produtoId = 1;

            //Act
            var myException = Assert.Throws<BadRequestException>(() => produtoService.AtualizarEstoque(produtoId, null));

            //Assert
            Assert.Equal("Dados inválidos.", myException.Message);
            produtoRepositoryMock.Verify(repo => repo.AtualizarEstoque(produtoId, It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Retorna_Erro_Not_Found_Produto_Inexistente()
        {
            // Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(It.IsAny<int>())).Returns(value: null);

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            int produtoId = 1;
            int quantidade = 10;

            // Act 
            var myException = Assert.Throws<NotFoundException>(() => produtoService.AtualizarEstoque(produtoId, quantidade));

            //Assert
            Assert.Equal("Produto não encontrado.", myException.Message);
            produtoRepositoryMock.Verify(repo => repo.AtualizarEstoque(produtoId, quantidade), Times.Never);
        }

        [Fact]
        public void Retorna_Produto_Atualiza_Estoque()
        {
            // Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Produto produto = new Produto
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: produto);

            int quantidade = 10;           

            produtoRepositoryMock.Setup(repo => repo.AtualizarEstoque(produtoId, quantidade));
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            //Act
            produtoService.AtualizarEstoque(produtoId, quantidade);

            //Assert
            produtoRepositoryMock.Verify(v => v.BuscaPorId(produtoId), Times.Once());
            produtoRepositoryMock.Verify(v => v.AtualizarEstoque(produtoId, quantidade), Times.Once());
        }

        //void Deletar(int id);
        [Fact]
        public void Retorna_Erro_Not_Found_Deleta_Produto_Inexistente()
        {
            // Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            
            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: null);

            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            // Act 
            var myException = Assert.Throws<NotFoundException>(() => produtoService.Deletar(produtoId));

            // Assert
            Assert.Equal("Produto não encontrado.", myException.Message);            
        }

        [Fact]
        public void Retorna_Deleta_Produto_Sucesso()
        {
            // Arrange
            Mock<IProdutoRepository> produtoRepositoryMock = new Mock<IProdutoRepository>();
            Mock<ICategoriaRepository> categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            Produto produto = new Produto
            {
                Nome = "Produto 1",
                Quantidade = 10,
                ValorVenda = 10.00m,
                CategoriaId = 1,
                Categoria = new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1",
                    Descricao = "Categoria descricao 1"
                }
            };

            int produtoId = 1;

            produtoRepositoryMock.Setup(repo => repo.BuscaPorId(produtoId)).Returns(value: produto);
            ProdutoService produtoService = new ProdutoService(produtoRepositoryMock.Object, categoriaRepositoryMock.Object);

            // Act 
            produtoService.Deletar(produtoId);

            // Assert
            produtoRepositoryMock.Verify(v => v.BuscaPorId(produtoId), Times.Once());
            produtoRepositoryMock.Verify(v => v.Excluir(produtoId), Times.Once());
        }
    }
}