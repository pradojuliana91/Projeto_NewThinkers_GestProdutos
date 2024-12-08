using GestProduto.API.Domain.Entity;
using System.Data.SqlClient;

namespace GestProduto.API.Domain.Repository.Impl
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public Usuario? Login(string nomeUsuario, string senha)
        {
            string sql = @"SELECT 
                                Id, 
                                Nome, 
                                Email,
                                Usuario,
                                Perfil
                           FROM 
                                USUARIOS 
                           WHERE 
                                Usuario = @NomeUsuario
                                AND Senha = @Senha";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NomeUsuario", nomeUsuario));
            parameters.Add(new SqlParameter("@Senha", senha));

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
                        string perfil = dataReader.GetString(dataReader.GetOrdinal("Perfil")).ToUpper();                       
                        Usuario usuario = new Usuario
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Nome = dataReader.GetString(dataReader.GetOrdinal("Nome")),
                            Email = dataReader.GetString(dataReader.GetOrdinal("Email")),
                            NomeUsuario = dataReader.GetString(dataReader.GetOrdinal("Usuario")),
                            Perfil = Enum.Parse<PerfilUsuarioEnum>(perfil)
                        };

                        return usuario;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao realizar login", ex);
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
