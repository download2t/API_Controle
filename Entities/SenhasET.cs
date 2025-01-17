using System.Data.SqlClient;
using System.Data;
using API_Loja.Entities;

namespace API_CONTROLE.Entities
{
    public class SenhasET
    {
        private Banco banco = new Banco();
        private CategoriaET aCTLCategorias;

        public SenhasET()
        {
            aCTLCategorias = new CategoriaET();

        }
        public void AdicionarSenha(Senhas senha)
        {
            try
            {
                string sql = "INSERT INTO Senhas (Login, Senha, Descricao, Link, CategoriaId) " +
                             "VALUES (@Login, @Senha, @Descricao, @Link, @CategoriaId)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Login", senha.Login),
                    new SqlParameter("@Senha", senha.Senha),
                    new SqlParameter("@Descricao", senha.Descricao),
                    new SqlParameter("@Link",senha.Link),
                    new SqlParameter("@CategoriaId", senha.Categoria.Id)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao adicionar a senha", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar a senha", ex);
            }
        }
        public void AtualizarSenha(Senhas senha)
        {
            try
            {
                string sql = "UPDATE Senhas SET Login = @Login, Senha = @Senha, " +
                             "Descricao = @Descricao, Link = @Link, CategoriaId = @CategoriaId WHERE Id = @Id";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Login", senha.Login),
                    new SqlParameter("@Senha", senha.Senha),
                    new SqlParameter("@Descricao", senha.Descricao),
                    new SqlParameter("@Link",senha.Link),
                    new SqlParameter("@CategoriaId", senha.Categoria.Id),
                    new SqlParameter("@Id", senha.Id)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao atualizar a senha", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar a senha", ex);
            }
        }
        public bool ExcluirSenha(int senhaId)
        {
            try
            {
                string sql = "DELETE FROM Senhas WHERE Id = @Id";
                SqlParameter[] parametros = { new SqlParameter("@Id", senhaId) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao excluir a senha", ex);

                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir a senha", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Senhas BuscarSenhaPorId(int id)
        {
            try
            {
                Senhas senha = null;
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Senhas WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            senha = new Senhas
                            {
                                Id = (int)reader["Id"],
                                Login = (string)reader["Login"],
                                Senha = (string)reader["Senha"],
                                Descricao = (string)reader["Descricao"],
                                Link = reader["Link"] != DBNull.Value ? (string)reader["Link"] : null,
                                Categoria = aCTLCategorias.BuscarCategoriaPorIdDeSenhas((int)reader["CategoriaId"])
                            };
                        }
                    }
                }

                return senha;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Buscar a senha por ID", ex);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Buscar a senha por ID", ex);
                return null;
            }
        }
        public List<Senhas> ListarSenhas()
        {
            try
            {
                List<Senhas> senhas = new List<Senhas>();
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "SELECT * FROM Senhas";
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Senhas senha = new Senhas
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Senha = (string)reader["Senha"],
                            Descricao = (string)reader["Descricao"],
                            Link = reader["Link"] != DBNull.Value ? (string)reader["Link"] : null,
                            Categoria = aCTLCategorias.BuscarCategoriaPorIdDeSenhas((int)reader["CategoriaId"])
                        };
                        senhas.Add(senha);
                    }
                }

                return senhas;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Listar as senhas", ex);
                return new List<Senhas>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Listar as senhas", ex);
                return new List<Senhas>();
            }
        }
        public List<Senhas> PesquisarSenhasPorCriterio(string criterio, string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Senhas WHERE ";
                List<SqlParameter> parametros = new List<SqlParameter>();

                if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                {
                    query += "Id = @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", id));
                }
                else if (criterio == "Categoria")
                {
                    query = "SELECT * FROM Senhas WHERE CategoriaId IN (SELECT Id FROM Categorias WHERE Nome LIKE @ValorPesquisa)";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else if (criterio == "Senha")
                {
                    query += "Senha LIKE @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else if (criterio == "Login")
                {
                    query += "Login LIKE @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else if (criterio == "Contem")
                {
                    query += "Id IN (SELECT Id FROM Senhas WHERE ";
                    query += "CategoriaId IN (SELECT Id FROM Categorias WHERE Nome LIKE @ValorPesquisa) OR ";
                    query += "Senha LIKE @ValorPesquisa OR ";
                    query += "Login LIKE @ValorPesquisa OR ";
                    query += "Descricao LIKE @ValorPesquisa OR ";
                    query += "Link LIKE @ValorPesquisa)";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else
                {
                    // Lógica para lidar com outros critérios, se necessário
                }

                DataTable dataTable = banco.ExecutarConsulta(query, parametros.ToArray());

                List<Senhas> senhas = new List<Senhas>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int categoriaId = Convert.ToInt32(row["CategoriaId"]);
                    Categoria categoria = aCTLCategorias.BuscarCategoriaPorIdDeSenhas(categoriaId);

                    Senhas senha = new Senhas
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Login = row["Login"].ToString(),
                        Senha = row["Senha"].ToString(),
                        Categoria = categoria,
                        Descricao = row["Descricao"].ToString(),
                        Link = row["Link"].ToString(),
                        // Adicione os demais campos da entidade Senhas aqui
                    };
                    senhas.Add(senha);
                }

                return senhas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar senhas por {criterio.ToLower()}", ex);
                return new List<Senhas>();
            }
        }



        public List<Senhas> PesquisarSenhasPorCriterio2(string criterio, string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Senhas WHERE ";
                List<SqlParameter> parametros = new List<SqlParameter>();

                if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                {
                    query += "Id = @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", id));
                }
                else if (criterio == "Categoria")
                {
                    query += "CategoriaId LIKE @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else if (criterio == "Senha")
                {
                    query += "Senha LIKE @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else if (criterio == "Login")
                {
                    query += "Login LIKE @ValorPesquisa";
                    parametros.Add(new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%"));
                }
                else
                {
                    // Lógica para lidar com outros critérios, se necessário
                }

                DataTable dataTable = banco.ExecutarConsulta(query, parametros.ToArray());

                List<Senhas> senhas = new List<Senhas>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int categoriaId = Convert.ToInt32(row["CategoriaId"]);
                    Categoria categoria = aCTLCategorias.BuscarCategoriaPorIdDeSenhas(categoriaId);

                    if (categoria != null)
                    {
                        senhas.Add(new Senhas
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Categoria = categoria,
                            Login = row["Login"].ToString(),
                            Senha = row["Senha"].ToString(),
                            Descricao = row["Descricao"].ToString(),
                            Link = row["Link"].ToString(),

                        });
                    }
                }

                return senhas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar senhas por {criterio.ToLower()}", ex);
                return new List<Senhas>();
            }
        }


    }
}
