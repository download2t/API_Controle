using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class SetoresET
    {
        private Banco banco = new Banco();

        public void AdicionarSetor(Setores setor)
        {
            try
            {
                string sql = "INSERT INTO Setores (Setor) VALUES (@Setor)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Setor", setor.Setor),
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("adicionar o setor", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("adicionar o setor", ex);
            }
        }

        public void AtualizarSetor(Setores setor)
        {
            try
            {
                string sql = "UPDATE Setores SET Setor = @Setor WHERE Id = @Id";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Setor", setor.Setor),
                    new SqlParameter("@Id", setor.Id),
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("atualizar o setor", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("atualizar o setor", ex);
            }
        }

        public bool ExcluirSetor(int setorId)
        {
            try
            {
                string sql = "DELETE FROM Setores WHERE Id = @Id";
                SqlParameter[] parametros = { new SqlParameter("@Id", setorId) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {
                    Console.WriteLine("Erro ao excluir o setor", ex);
                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir o setor", ex);
                return false; // Retorne false para indicar falha
            }
        }

        public Setores BuscarSetorPorId(int id)
        {
            try
            {
                Setores setor = null;
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Setores WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            setor = new Setores
                            {
                                Id = (int)reader["Id"],
                                Setor = (string)reader["Setor"],
                            };
                        }
                    }
                }

                return setor;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("buscar o setor por ID", ex);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("buscar o setor por ID", ex);
                return null;
            }
        }
        public List<Setores> BuscarSetorPorNome(string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Setores WHERE Setor LIKE @ValorPesquisa";
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                List<Setores> setor = new List<Setores>();
                foreach (DataRow row in dataTable.Rows)
                {
                    setor.Add(new Setores
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Setor = row["Setor"].ToString()
                    });
                }

                return setor;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar categorias por nome", ex);
                return new List<Setores>();
            }
        }

        public List<Setores> ListarSetores()
        {
            try
            {
                List<Setores> setores = new List<Setores>();
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "SELECT * FROM Setores Order By Id Desc";
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Setores setor = new Setores
                        {
                            Id = (int)reader["Id"],
                            Setor = (string)reader["Setor"],
                        };
                        setores.Add(setor);
                    }
                }

                return setores;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("listar os setores", ex);
                return new List<Setores>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("listar os setores", ex);
                return new List<Setores>();
            }
        }
    }
}
