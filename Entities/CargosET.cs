using API_Loja.Repository;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace API_Loja.Entities
{
    public class CargosET
    {
        private Banco banco = new Banco();

        public bool AdicionarCargo(Cargo cargo)
        {
            try
            {
                string sql = "INSERT INTO Cargos (Funcao, Pontos) VALUES (@Funcao, @Pontos)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Funcao", cargo.Funcao),
            new SqlParameter("@Pontos", cargo.Pontos)
        };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar cargo: ", ex); // Corrigido para concatenar a mensagem de erro
                return false; // Retorna false se ocorrer uma exceção
            }
        }
        public bool AtualizarCargo(Cargo cargo)
        {
            try
            {
                string sql = "UPDATE Cargos SET Funcao = @Funcao, Pontos = @Pontos WHERE Id = @Id";
                SqlParameter[] parametros =
                {
            new SqlParameter("@Funcao", cargo.Funcao),
            new SqlParameter("@Pontos", cargo.Pontos),
            new SqlParameter("@Id", cargo.Id)
        };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar cargo: ", ex); // Corrigido para concatenar a mensagem de erro
                return false; // Retorna false se ocorrer uma exceção
            }
        }
        public bool ExcluirCargo(int cargoId)
        {
            try
            {
                string sql = "DELETE FROM Cargos WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", cargoId);
                banco.ExecutarComando(sql, new[] { parametro });
                return true; // Retorne true para indicar sucesso
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir cargo", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Cargo BuscarCargoPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Cargos WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateCargoFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar cargo por ID", ex);
                return null;
            }
        }
        public List<Cargo> ListarCargos()
        {
            try
            {
                string sql = "SELECT * FROM Cargos ORDER BY Id DESC";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                return CreateCargosListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar cargos", ex);
                return new List<Cargo>();
            }
        }
        private Cargo CreateCargoFromDataRow(DataRow row)
        {
            return new Cargo
            {
                Id = Convert.ToInt32(row["Id"]),
                Funcao = row["Funcao"].ToString(),
                Pontos = Convert.ToInt32(row["Pontos"])
            };
        }
        private List<Cargo> CreateCargosListFromDataTable(DataTable dataTable)
        {
            List<Cargo> cargos = new List<Cargo>();
            foreach (DataRow row in dataTable.Rows)
            {
                cargos.Add(CreateCargoFromDataRow(row));
            }
            return cargos;
        }
        public List<Cargo> BuscarCargosPorPontos(string pontos)
        {
            try
            {
                string query = "SELECT * FROM Cargos WHERE Pontos = @Pontos";
                SqlParameter parametro = new SqlParameter("@Pontos", pontos);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                return CreateCargosListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar cargos por pontos", ex);
                return new List<Cargo>();
            }
        }
        public List<Cargo> BuscarCargosPorNome(string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Cargos WHERE Funcao LIKE @ValorPesquisa";
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                return CreateCargosListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar cargos por função", ex);
                return new List<Cargo>();
            }
        }
    }
}
