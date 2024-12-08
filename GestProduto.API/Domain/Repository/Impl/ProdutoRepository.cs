using GestProduto.API.Domain.Entity;
using System.Data.SqlClient;

namespace GestProduto.API.Domain.Repository.Impl
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public Produto? BuscaPorId(int id)
        {
            string sql = @"SELECT 
                                p.Id, 
                                p.Nome, 
                                p.Quantidade, 
                                p.ValorVenda,
                                c.Id as CategoriaId,
                                c.Nome as CategoriaNome,
                                c.Descricao as CategoriaDescricao
                           FROM 
                                PRODUTOS p 
                                INNER JOIN CATEGORIAS c ON c.ID = p.CategoriaId
                           WHERE 
                                p.Id = @Id";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Id", id));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddRange(parameters.ToArray());

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Produto produto = new Produto()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            ValorVenda = dataReader.GetDecimal(dataReader.GetOrdinal("ValorVenda")),
                            CategoriaId = dataReader.GetInt32(dataReader.GetOrdinal("CategoriaId")),
                            Categoria = new Categoria()
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("CategoriaId")),
                                Nome = dataReader.GetString(dataReader.GetOrdinal("CategoriaNome")),
                                Descricao = dataReader.GetString(dataReader.GetOrdinal("CategoriaDescricao")),
                            }
                        };
                        return produto;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<Produto>? BuscarProdutos(int? id, string? nome, int? categoriaId, bool? disponivel)
        {
            string sql = @"SELECT 
                                p.Id, 
                                p.Nome, 
                                p.Quantidade, 
                                p.ValorVenda,
                                c.Id as CategoriaId,
                                c.Nome as CategoriaNome,
                                c.Descricao as CategoriaDescricao
                           FROM 
                                PRODUTOS p 
                                INNER JOIN CATEGORIAS c ON c.ID = p.CategoriaId
                           WHERE 
                                1 = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (id != null)
            {
                sql += " AND p.Id = @Id";
                parameters.Add(new SqlParameter("@Id", id));
            }

            if (!string.IsNullOrEmpty(nome))
            {
                sql += " AND p.Nome LIKE @Nome";
                parameters.Add(new SqlParameter("@nome", "%" + nome + "%"));
            }

            if (categoriaId != null)
            {
                sql += " AND c.Id = @CategoriaId";
                parameters.Add(new SqlParameter("@CategoriaId", categoriaId));
            }

            if (disponivel != null && disponivel == true)
            {
                sql += " AND p.Quantidade > 0";
            }

            if (disponivel != null && disponivel == false)
            {
                sql += " AND p.Quantidade = 0";
            }

            List<Produto> produtos = new List<Produto>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddRange(parameters.ToArray());

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Produto produto = new Produto()
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Quantidade = dataReader.GetInt32(dataReader.GetOrdinal("Quantidade")),
                            ValorVenda = dataReader.GetDecimal(dataReader.GetOrdinal("ValorVenda")),
                            CategoriaId = dataReader.GetInt32(dataReader.GetOrdinal("CategoriaId")),
                            Categoria = new Categoria()
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("CategoriaId")),
                                Nome = dataReader.GetString(dataReader.GetOrdinal("CategoriaNome")),
                                Descricao = dataReader.GetString(dataReader.GetOrdinal("CategoriaDescricao")),
                            }
                        };

                        produtos.Add(produto);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar produtos", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return produtos;
        }

        public int Salvar(Produto produto)
        {
            string sql = @"INSERT INTO PRODUTOS (Nome, Quantidade, ValorVenda, CategoriaId)
                           VALUES (@Nome, @Quantidade, @ValorVenda, @CategoriaId);
                           SELECT CONVERT(int,SCOPE_IDENTITY())";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
                    command.Parameters.AddWithValue("@CategoriaId", produto.CategoriaId);

                    int idProduto = (int)command.ExecuteScalar();
                    return idProduto;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Atualizar(int id, Produto produto)
        {
            string sql = @"UPDATE PRODUTOS SET Nome = @Nome, Quantidade = @Quantidade, ValorVenda = @ValorVenda, CategoriaId = @CategoriaId WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
                    command.Parameters.AddWithValue("@CategoriaId", produto.CategoriaId);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AtualizarEstoque(int id, int quantidade)
        {
            string sql = @"UPDATE PRODUTOS SET Quantidade = @Quantidade WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Quantidade", quantidade);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Excluir(int id)
        {
            string sql = @"DELETE FROM PRODUTOS WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception("Erro ao excluir produto", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
