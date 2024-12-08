using GestProduto.API.Domain.Entity;
using System.Data.SqlClient;

namespace GestProduto.API.Domain.Repository.Impl
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string connectionString;

        public CategoriaRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<Categoria>? Listar()
        {
            string sql = @"SELECT 
                                Id, 
                                Nome, 
                                Descricao 
                           FROM 
                                CATEGORIAS 
                           ORDER BY 
                                Nome";

            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Categoria categoria = new Categoria
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Descricao = dataReader.GetString(dataReader.GetOrdinal("Descricao")),
                        };

                        categorias.Add(categoria);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar categorias", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return categorias;
        }

        public Categoria? BuscaPorId(int id)
        {
            string sql = @"SELECT 
                                Id, 
                                Nome, 
                                Descricao 
                           FROM 
                                CATEGORIAS
                           WHERE
                                Id = @Id";

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
                        Categoria categoria = new Categoria
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Descricao = dataReader.GetString(dataReader.GetOrdinal("Descricao")),
                        };

                        return categoria;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar categoria", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }
    }
}
