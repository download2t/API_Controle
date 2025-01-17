using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace API_CONTROLE.Entities
{
    public class ContatosET
    {
        private readonly Banco banco;

        public ContatosET()
        {
            banco = new Banco(); // Inicialize o objeto Banco ou injete-o conforme necessário
        }

        public bool AdicionarContato(Contatos contato)
        {
            try
            {
                string sql = "INSERT INTO Contatos (Nome, Numero) VALUES (@Nome, @Numero)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", contato.Nome),
                    new SqlParameter("@Numero", contato.Numero)
                };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                // Log ou manipule a exceção conforme necessário
                Console.WriteLine("Erro ao adicionar contato: " + ex.Message);
                return false; // Retorna false em caso de erro
            }
        }

        public bool AtualizarContato(Contatos contato)
        {
            try
            {
                string sql = "UPDATE Contatos SET Nome = @Nome, Numero = @Numero WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", contato.Nome),
                    new SqlParameter("@Numero", contato.Numero),
                    new SqlParameter("@Id", contato.Id)
                };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                // Log ou manipule a exceção conforme necessário
                Console.WriteLine("Erro ao atualizar contato: " + ex.Message);
                return false; // Retorna false em caso de erro
            }
        }

        public bool ExcluirContato(int contatoId)
        {
            try
            {
                string sql = "DELETE FROM Contatos WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", contatoId);
                banco.ExecutarComando(sql, new[] { parametro });
                return true; // Retorne true para indicar sucesso
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir contato: " + ex.Message);
                return false; // Retorne false para indicar falha
            }
        }

        public Contatos BuscarContatoPorId(int id)
        {
            try
            {
                string query = "SELECT Id, Nome, Numero FROM Contatos WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateContatoFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar contato por ID: " + ex.Message);
                return null;
            }
        }

        public List<Contatos> ListarContatos()
        {
            try
            {
                string sql = "SELECT Id, Nome, Numero FROM Contatos ORDER BY Id DESC";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                return CreateContatosListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar contatos: " + ex.Message);
                return new List<Contatos>();
            }
        }

        public List<Contatos> PesquisarContatosPorCriterio(string criterio, string valorPesquisa)
        {
            List<Contatos> contatosEncontrados = new List<Contatos>();

            try
            {
                string query = string.Empty;
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");

                // Verificando o critério de pesquisa
                if (criterio == "Nome")
                {
                    query = "SELECT Id, Nome, Numero FROM Contatos WHERE Nome LIKE @ValorPesquisa";
                }
                else if (criterio == "Numero")
                {
                    query = "SELECT Id, Nome, Numero FROM Contatos WHERE Numero LIKE @ValorPesquisa";
                }
                else if (criterio == "Id")
                {
                    query = "SELECT Id, Nome, Numero FROM Contatos WHERE Id = @ValorPesquisa"; // Nota: Use '=' ao invés de 'LIKE' para ID
                }

                // Executar a consulta e preencher a lista de contatos encontrados
                if (!string.IsNullOrEmpty(query))
                {
                    DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Contatos contato = CreateContatoFromDataRow(row);
                        contatosEncontrados.Add(contato);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao pesquisar contatos por critério: " + ex.Message);
                return new List<Contatos>();
            }

            return contatosEncontrados;
        }

        private Contatos CreateContatoFromDataRow(DataRow row)
        {
            return new Contatos
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                Numero = row["Numero"].ToString()
            };
        }

        private List<Contatos> CreateContatosListFromDataTable(DataTable dataTable)
        {
            List<Contatos> contatos = new List<Contatos>();
            foreach (DataRow row in dataTable.Rows)
            {
                contatos.Add(CreateContatoFromDataRow(row));
            }
            return contatos;
        }
    }
}
