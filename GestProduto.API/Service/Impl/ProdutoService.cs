using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;
using GestProduto.API.Domain.Entity;
using GestProduto.API.Domain.Repository;
using GestProduto.API.Exceptions;

namespace GestProduto.API.Service.Impl
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            this.produtoRepository = produtoRepository;
            this.categoriaRepository = categoriaRepository;
        }

        public List<CategoriaResponse> ListarCategoriasDeProduto()
        {
            List<Categoria>? categorias = categoriaRepository.Listar();

            if (categorias == null || categorias.Count == 0)
            {
                throw new NotFoundException("Nenhuma categoria encontrada.");
            }

            List<CategoriaResponse> categoriasResponse = new List<CategoriaResponse>();
            foreach (var categoria in categorias)
            {
                categoriasResponse.Add(new CategoriaResponse
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome!,
                    Descricao = categoria.Descricao!
                });
            }
            return categoriasResponse;
        }

        public ProdutoResponse BuscaPorId(int id)
        {
            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            return new ProdutoResponse()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                ValorVenda = produto.ValorVenda,
                Disponivel = (produto.Quantidade > 0),
                Categoria = new CategoriaResponse()
                {
                    Id = produto.Categoria.Id,
                    Nome = produto.Categoria.Nome,
                    Descricao = produto.Categoria.Descricao
                }
            };
        }

        public List<ProdutoResponse> BuscarProdutos(int? id, string? nome, int? categoriaId, bool? disponivel)
        {
            List<Produto>? produtos = produtoRepository.BuscarProdutos(id, nome, categoriaId, disponivel);

            if (produtos == null || produtos.Count == 0)
            {
                throw new NotFoundException("Nenhum produto encontrado.");
            }

            List<ProdutoResponse> produtosResponse = new List<ProdutoResponse>();
            foreach (var produto in produtos)
            {
                produtosResponse.Add(new ProdutoResponse
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    ValorVenda = produto.ValorVenda,
                    Disponivel = (produto.Quantidade > 0),
                    Categoria = new CategoriaResponse()
                    {
                        Id = produto.Categoria.Id,
                        Nome = produto.Categoria.Nome,
                        Descricao = produto.Categoria.Descricao
                    }
                });
            }
            return produtosResponse;
        }

        public ProdutoCadastroResponse Cadastrar(ProdutoRequest produtoRequest)
        {
            if (produtoRequest == null ||
                string.IsNullOrEmpty(produtoRequest.Nome) ||
                produtoRequest.Quantidade == null || produtoRequest.Quantidade < 0 ||
                produtoRequest.ValorVenda == null || produtoRequest.ValorVenda <= 0 ||
                produtoRequest.CategoriaId == null || produtoRequest.CategoriaId <= 0)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Categoria? categoria = categoriaRepository.BuscaPorId(produtoRequest.CategoriaId.Value);

            if (categoria == null)
            {
                throw new BadRequestException("Categoria inválida.");
            }

            int idProduto = produtoRepository.Salvar(new Produto()
            {
                Nome = produtoRequest.Nome,
                Quantidade = produtoRequest.Quantidade.Value,
                ValorVenda = produtoRequest.ValorVenda.Value,
                CategoriaId = categoria.Id,
                Categoria = new Categoria()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                }
            });

            return new ProdutoCadastroResponse
            {
                Id = idProduto
            };
        }

        public void Atualizar(int id, ProdutoRequest produtoRequest)
        {
            if (produtoRequest == null ||
                string.IsNullOrEmpty(produtoRequest.Nome) ||
                produtoRequest.Quantidade == null || produtoRequest.Quantidade < 0 ||
                produtoRequest.ValorVenda == null || produtoRequest.ValorVenda <= 0 ||
                produtoRequest.CategoriaId == null || produtoRequest.CategoriaId <= 0)
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Categoria? categoria = categoriaRepository.BuscaPorId(produtoRequest.CategoriaId.Value);

            if (categoria == null)
            {
                throw new BadRequestException("Categoria inválida.");
            }

            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            produto.Nome = produtoRequest.Nome;
            produto.Quantidade = produtoRequest.Quantidade.Value;
            produto.ValorVenda = produtoRequest.ValorVenda.Value;
            produto.CategoriaId = categoria.Id;
            produto.Categoria = new Categoria()
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao
            };

            produtoRepository.Atualizar(id, produto);

        }

        public void AtualizarEstoque(int id, int? quantidade)
        {
            if (quantidade == null || quantidade < 0) 
            {
                throw new BadRequestException("Dados inválidos.");
            }

            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            produtoRepository.AtualizarEstoque(id, quantidade.Value);
        }

        public void Deletar(int id)
        {
            Produto? produto = produtoRepository.BuscaPorId(id);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            produtoRepository.Excluir(id);
        }
    }
}
