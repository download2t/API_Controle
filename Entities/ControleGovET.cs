using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class ControleGovET
    {
        Banco banco;
        FuncionariosET aCTLFuncionarios;
        public ControleGovET()
        {
            banco = new Banco();
            aCTLFuncionarios = new FuncionariosET();
        }

        public string AdicionarControleGov(ControleGov ControleGov)
        {
            try
            {
                string sql = "INSERT INTO ControleGov (Data, PermaneceEntrada, SaidasEntrada, ReservadasRealizadas, PermaneceRealizadas, SaidasRealizadas, Realizados, Porcentagem, FuncionarioID) " +
                             "VALUES (@Data, @PermaneceEntrada, @SaidasEntrada, @ReservadasRealizadas, @PermaneceRealizadas, @SaidasRealizadas, @Realizados, @Porcentagem, @FuncionarioID)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Data", ControleGov.Data),
                    new SqlParameter("@PermaneceEntrada", ControleGov.PermaneceEntrada),
                    new SqlParameter("@SaidasEntrada", ControleGov.SaidasEntrada),
                    new SqlParameter("@ReservadasRealizadas", ControleGov.ReservadasRealizadas),
                    new SqlParameter("@PermaneceRealizadas", ControleGov.PermaneceRealizadas),
                    new SqlParameter("@SaidasRealizadas", ControleGov.SaidasRealizadas),
                    new SqlParameter("@Realizados", ControleGov.Realizados),
                    new SqlParameter("@Porcentagem", ControleGov.Porcentagem),
                    new SqlParameter("@FuncionarioID", ControleGov.Funcionarios.Id)
                };
                banco.ExecutarComando(sql, parametros);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AtualizarControleGov(ControleGov ControleGov)
        {
            try
            {
                string sql = "UPDATE ControleGov SET Data = @Data, PermaneceEntrada = @PermaneceEntrada, SaidasEntrada = @SaidasEntrada, " +
                             "ReservadasRealizadas = @ReservadasRealizadas, PermaneceRealizadas = @PermaneceRealizadas, SaidasRealizadas = @SaidasRealizadas, " +
                             "Realizados = @Realizados, Porcentagem = @Porcentagem, FuncionarioID = @FuncionarioID WHERE ID = @ID";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Data", ControleGov.Data),
                    new SqlParameter("@PermaneceEntrada", ControleGov.PermaneceEntrada),
                    new SqlParameter("@SaidasEntrada", ControleGov.SaidasEntrada),
                    new SqlParameter("@ReservadasRealizadas", ControleGov.ReservadasRealizadas),
                    new SqlParameter("@PermaneceRealizadas", ControleGov.PermaneceRealizadas),
                    new SqlParameter("@SaidasRealizadas", ControleGov.SaidasRealizadas),
                    new SqlParameter("@Realizados", ControleGov.Realizados),
                    new SqlParameter("@Porcentagem", ControleGov.Porcentagem),
                    new SqlParameter("@FuncionarioID", ControleGov.Funcionarios.Id),
                    new SqlParameter("@ID", ControleGov.Id)
                };
                banco.ExecutarComando(sql, parametros);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool ExcluirControleGov(int controleID)
        {
            try
            {
                string sql = "DELETE FROM ControleGov WHERE ID = @ID";
                SqlParameter parametro = new SqlParameter("@ID", controleID);
                banco.ExecutarComando(sql, new[] { parametro });
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao excluir controle governamental", ex);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir controle governamental", ex);
                return false;
            }
        }

        public ControleGov BuscarControleGovPorID(int controleID)
        {
            try
            {
                string query = "SELECT * FROM ControleGov WHERE ID = @ID";
                SqlParameter parametro = new SqlParameter("@ID", controleID);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateControleGovFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar controle governamental por ID", ex);
                return null;
            }
        }
        public List<ControleGov> ListarControleGov()
        {
            try
            {
                string sql = "SELECT * FROM ControleGov";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                return CreateControleGovListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar controle governamental", ex);
                return new List<ControleGov>();
            }
        }

        public List<ControleGov> ListarControleGov(DateTime? dataEntrada = null, DateTime? dataSaida = null, string nomeFuncionario = "")
        {
            try
            {
                // Definindo a data inicial padrão para os últimos 31 dias, caso não seja especificada
                if (dataEntrada == null || dataSaida == null)
                {
                    dataEntrada = DateTime.Now.AddDays(-31).Date; // Considera apenas a data, sem o horário
                    dataSaida = DateTime.Now.Date; // Considera apenas a data, sem o horário
                }

                string sql = "SELECT ControleGov.*, Funcionarios.Nome AS FuncionarioNome " +
                             "FROM ControleGov " +
                             "INNER JOIN Funcionarios ON ControleGov.FuncionarioID = Funcionarios.Id " +
                             "WHERE 1=1 ";

                // Adicionando a condição de data, se fornecida
                if (dataEntrada != null && dataSaida != null)
                {
                    sql += "AND ControleGov.Data >= @DataEntrada AND ControleGov.Data < DATEADD(day, 1, @DataSaida) ";
                }

                // Adicionando a condição de nome do funcionário, se fornecida
                if (!string.IsNullOrEmpty(nomeFuncionario))
                {
                    sql += "AND Funcionarios.Nome LIKE @NomeFuncionario ";
                }

                sql += "ORDER BY ControleGov.ID DESC";

                SqlCommand command = new SqlCommand(sql, banco.Abrir());

                // Definindo os parâmetros de data, se fornecidos
                if (dataEntrada != null && dataSaida != null)
                {
                    command.Parameters.AddWithValue("@DataEntrada", dataEntrada);
                    command.Parameters.AddWithValue("@DataSaida", dataSaida);
                }

                // Definindo o parâmetro de nome do funcionário, se fornecido
                if (!string.IsNullOrEmpty(nomeFuncionario))
                {
                    command.Parameters.AddWithValue("@NomeFuncionario", "%" + nomeFuncionario + "%");
                }

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                return CreateControleGovListFromDataTable(dataTable);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao listar controle governamental", ex);
                return new List<ControleGov>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar controle governamental", ex);
                return new List<ControleGov>();
            }
        }




        private ControleGov CreateControleGovFromDataRow(DataRow row)
        {
            Funcionario func = aCTLFuncionarios.BuscarFuncionarioPorId(Convert.ToInt32(row["FuncionarioID"]));
            return new ControleGov
            {
                Id = Convert.ToInt32(row["ID"]),
                Data = Convert.ToDateTime(row["Data"]),
                PermaneceEntrada = Convert.ToInt32(row["PermaneceEntrada"]),
                SaidasEntrada = Convert.ToInt32(row["SaidasEntrada"]),
                ReservadasRealizadas = Convert.ToInt32(row["ReservadasRealizadas"]),
                PermaneceRealizadas = Convert.ToInt32(row["PermaneceRealizadas"]),
                SaidasRealizadas = Convert.ToInt32(row["SaidasRealizadas"]),
                Realizados = Convert.ToInt32(row["Realizados"]),
                Porcentagem = Convert.ToDecimal(row["Porcentagem"]),
                Funcionarios = func
            };
        }

        private List<ControleGov> CreateControleGovListFromDataTable(DataTable dataTable)
        {
            List<ControleGov> controles = new List<ControleGov>();

            foreach (DataRow row in dataTable.Rows)
            {
                controles.Add(CreateControleGovFromDataRow(row));
            }

            return controles;
        }
    }
}
