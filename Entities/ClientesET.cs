using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class ClientesET
    {
        private Banco banco = new Banco();

        public bool AdicionarCliente(Clientes cliente)
        {
            try
            {
                string sql = "INSERT INTO Clientes (Nome, Documento, Telefone, Email, Cep, Logradouro, Numero, Bairro, Cidade, UF) " +
                             "VALUES (@Nome, @Documento, @Telefone, @Email, @Cep, @Logradouro, @Numero, @Bairro, @Cidade, @UF)";
                SqlParameter[] parametros =
                {
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Documento", cliente.Documento),
                new SqlParameter("@Telefone", cliente.Telefone),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Cep", cliente.Cep),
                new SqlParameter("@Logradouro", cliente.Logradouro),
                new SqlParameter("@Numero", cliente.Numero),
                new SqlParameter("@Bairro", cliente.Bairro),
                new SqlParameter("@Cidade", cliente.Cidade),
                new SqlParameter("@UF", cliente.UF)
            };
                banco.ExecutarComando(sql, parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar cliente", ex);
                return false;
            }
        }

        public bool AtualizarCliente(Clientes cliente)
        {
            try
            {
                string sql = "UPDATE Clientes SET Nome = @Nome, Documento = @Documento, " +
                             "Telefone = @Telefone, Email = @Email, Cep = @Cep, " +
                             "Logradouro = @Logradouro, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, UF = @UF WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Documento", cliente.Documento),
                new SqlParameter("@Telefone", cliente.Telefone),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Cep", cliente.Cep),
                new SqlParameter("@Logradouro", cliente.Logradouro),
                new SqlParameter("@Numero", cliente.Numero),
                new SqlParameter("@Bairro", cliente.Bairro),
                new SqlParameter("@Cidade", cliente.Cidade),
                new SqlParameter("@UF", cliente.UF),
                new SqlParameter("@Id", cliente.Id)
            };
                banco.ExecutarComando(sql, parametros);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar cliente", ex);
                return false;
            }
        }
        public bool ExcluirCliente(int clienteId)
        {
            try
            {
                string sql = "DELETE FROM Clientes WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", clienteId);
                banco.ExecutarComando(sql, new[] { parametro });
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao excluir cliente", ex);

                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir cliente", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Clientes BuscarClientePorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Clientes WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateClienteFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar cliente por ID", ex);
                return null;
            }
        }
        public List<Clientes> ListarClientes()
        {
            try
            {
                string sql = "SELECT * FROM Clientes Order By Id Desc";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                return CreateClientesListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar clientes", ex);
                return new List<Clientes>();
            }
        }
        private Clientes CreateClienteFromDataRow(DataRow row)
        {
            return new Clientes
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                Documento = row["Documento"].ToString(),
                Telefone = row["Telefone"].ToString(),
                Email = row["Email"].ToString(),
                Cep = row["Cep"].ToString(),
                Cidade = row["Cidade"].ToString(),
                Bairro = row["Bairro"].ToString(),
                Logradouro = row["Logradouro"].ToString(),
                Numero = Convert.ToInt32(row["Numero"]),
                UF = row["Uf"].ToString()
            };
        }
        private List<Clientes> CreateClientesListFromDataTable(DataTable dataTable)
        {
            List<Clientes> clientes = new List<Clientes>();
            foreach (DataRow row in dataTable.Rows)
            {
                clientes.Add(CreateClienteFromDataRow(row));
            }
            return clientes;
        }

        public List<Clientes> PesquisarClientesPorCriterio(string criterio, string valorPesquisa)
        {
            try
            {
                string query = string.Empty;
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");

                if (criterio == "Nome")
                {
                    query = "SELECT * FROM Clientes WHERE Nome LIKE @ValorPesquisa";
                }
                else if (criterio == "Documento")
                {
                    query = "SELECT * FROM Clientes WHERE Documento LIKE @ValorPesquisa";
                }
                else if (criterio == "Email")
                {
                    query = "SELECT * FROM Clientes WHERE Email LIKE @ValorPesquisa";
                }
                else if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                {
                    query = "SELECT * FROM Clientes WHERE Id = @ValorPesquisa";
                    parametro = new SqlParameter("@ValorPesquisa", id);
                }
                else
                {
                    // Trate aqui o caso em que o critério não é reconhecido.
                    // Pode ser lançada uma exceção ou tratado de outra forma apropriada.
                    return new List<Clientes>();
                }

                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });
                return CreateClientesListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao pesquisar clientes por {criterio.ToLower()}", ex);
                return new List<Clientes>();
            }
        }


    }
}
