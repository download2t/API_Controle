using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class ManutencaoET
    {

        private Banco banco = new Banco();
        private PatrimoniosET aCTLPatrimonios = new PatrimoniosET();

        public void AdicionarManutencao(Manutencao manutencao)
        {
            try
            {
                string sql = "INSERT INTO Manutencao (PatrimonioId, Data_Reparo, Valor_Conserto, Descricao, Profissional, Telefone) " +
                             "VALUES (@PatrimonioId, @Data_Reparo, @Valor_Conserto, @Descricao, @Profissional, @Telefone)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@PatrimonioId", manutencao.Patrimonio.Id),
                    new SqlParameter("@Data_Reparo", (object)manutencao.DataReparo ?? DBNull.Value),
                    new SqlParameter("@Valor_Conserto", (object)manutencao.ValorConserto ?? DBNull.Value),
                    new SqlParameter("@Profissional", manutencao.Profissional),
                    new SqlParameter("@Telefone", manutencao.Telefone),
                    new SqlParameter("@Descricao", manutencao.Descricao)
                };
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar manutenção", ex);
            }
        }
        public void AtualizarManutencao(Manutencao manutencao)
        {
            try
            {
                string sql = "UPDATE Manutencao " +
                             "SET PatrimonioId = @PatrimonioId, Data_Reparo = @Data_Reparo, " +
                             "Valor_Conserto = @Valor_Conserto, Descricao = @Descricao, Profissional = @Profissional, " +
                             "Telefone = @Telefone " +
                             "WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@PatrimonioId", manutencao.Patrimonio.Id),
                    new SqlParameter("@Data_Reparo", (object)manutencao.DataReparo ?? DBNull.Value),
                    new SqlParameter("@Valor_Conserto", (object)manutencao.ValorConserto ?? DBNull.Value),
                    new SqlParameter("@Profissional", manutencao.Profissional),
                    new SqlParameter("@Telefone", manutencao.Telefone),
                    new SqlParameter("@Descricao", manutencao.Descricao),
                    new SqlParameter("@Id", manutencao.Id)
                };
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar manutenção", ex);
            }
        }
        public bool ExcluirManutencao(int manutencaoId)
        {
            try
            {
                string sql = "DELETE FROM Manutencao WHERE Id = @Id";
                SqlParameter[] parametros = { new SqlParameter("@Id", manutencaoId) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {

                    Console.WriteLine("Erro ao excluir manutenção", ex);
                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir manutenção", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Manutencao BuscarManutencaoPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Manutencao WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                  
                    Patrimonios patrimonio = aCTLPatrimonios.BuscarPatrimonioPorId((int)row["PatrimonioId"]);

                    return new Manutencao
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Patrimonio = patrimonio,
                        DataReparo = row.Field<DateTime?>("Data_Reparo"),
                        ValorConserto = Convert.ToDecimal(row["Valor_Conserto"]),
                        Descricao = row["Descricao"].ToString(),
                        Profissional = row["Profissional"].ToString(),
                        Telefone = row["Telefone"].ToString()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar manutenção por ID", ex);
                return null;
            }
        }
        public List<Manutencao> ListarManutencoes(DateTime? dataInicio = null, DateTime? dataFim = null, int mes = 0, int ano = 0)
        {
            try
            {
                List<Manutencao> manutencoes = new List<Manutencao>();
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "SELECT * FROM Manutencao WHERE 1=1";

                    if (dataInicio != null && dataFim != null)
                    {
                        sql += " AND DataReparo >= @DataInicio AND DataReparo <= @DataFim";
                    }
                    else if (mes != 0 && ano != 0)
                    {
                        sql += " AND MONTH(DataReparo) = @Mes AND YEAR(DataReparo) = @Ano";
                    }

                    // Adicione a cláusula ORDER BY para ordenar por ID em ordem decrescente
                    sql += " ORDER BY ID DESC";

                    SqlCommand command = new SqlCommand(sql, connection);

                    if (dataInicio != null && dataFim != null)
                    {
                        command.Parameters.AddWithValue("@DataInicio", dataInicio);
                        command.Parameters.AddWithValue("@DataFim", dataFim);
                    }
                    else if (mes != 0 && ano != 0)
                    {
                        command.Parameters.AddWithValue("@Mes", mes);
                        command.Parameters.AddWithValue("@Ano", ano);
                    }

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Patrimonios patrimonio = aCTLPatrimonios.BuscarPatrimonioPorId((int)reader["PatrimonioId"]);

                        Manutencao manutencao = new Manutencao
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Patrimonio = patrimonio,
                            DataReparo = (DateTime)reader["Data_Reparo"],
                            ValorConserto = Convert.ToDecimal(reader["Valor_Conserto"]),
                            Descricao = reader["Descricao"].ToString(),
                            Profissional = reader["Profissional"].ToString(),
                            Telefone = reader["Telefone"].ToString()
                        };

                        manutencoes.Add(manutencao);
                    }
                }

                return manutencoes;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao listar manutenções", ex);
                return new List<Manutencao>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar manutenções", ex);
                return new List<Manutencao>();
            }
        }
        public List<Manutencao> PesquisarManutencoesPorCriterio(string criterio, string valorPesquisa)
        {
            List<Manutencao> manutencoesEncontradas = new List<Manutencao>();

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = string.Empty;
                    SqlCommand command = new SqlCommand();

                    if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                    {
                        query = "SELECT * FROM Manutencao WHERE Id = @ValorPesquisa";
                        command.Parameters.AddWithValue("@ValorPesquisa", id);
                    }
                    else if (criterio == "Patrimonio")
                    {
                        query = "SELECT * FROM Manutencao WHERE PatrimonioId IN (SELECT Id FROM Patrimonios WHERE Patrimonio LIKE @ValorPesquisa)";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }

                    command.CommandText = query;
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Manutencao manutencao = new Manutencao
                            {
                                Id = (int)reader["Id"],
                                Patrimonio = aCTLPatrimonios.BuscarPatrimonioPorId((int)reader["PatrimonioId"]),
                                DataReparo = (DateTime)reader["Data_Reparo"],
                                Descricao = (string)reader["Descricao"],
                                Profissional = (string)reader["Profissional"],
                                Telefone = (string)reader["Telefone"],
                                ValorConserto = (decimal)reader["Valor_Conserto"]
                            };
                            manutencoesEncontradas.Add(manutencao);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Pesquisar manutenções por critério", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pesquisar manutenções por critério", ex);
            }

            return manutencoesEncontradas;
        }
        public DataTable RelatorioManutencao2(string procurar = "", DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            try
            {
                string sql = @"SELECT P.Id AS IdPatrimonio,
                             P.Patrimonio,
                             M.Data_Reparo AS DataReparo,
                             M.Descricao,
                             M.Profissional,
                             M.Valor_Conserto as ValorConserto
                      FROM Patrimonios P
                      INNER JOIN Manutencao M ON P.Id = M.PatrimonioId
                      WHERE 1 = 1";

                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(procurar))
                {
                    whereConditions.Add("P.Patrimonio LIKE @procurar");
                    parameters.Add(new SqlParameter("@procurar", $"%{procurar}%"));
                }

                if (dataInicial.HasValue)
                {
                    whereConditions.Add("M.Data_Reparo >= @dataInicial");
                    parameters.Add(new SqlParameter("@dataInicial", dataInicial.Value.Date));
                }

                if (dataFinal.HasValue)
                {
                    whereConditions.Add("M.Data_Reparo <= @dataFinal");
                    parameters.Add(new SqlParameter("@dataFinal", dataFinal.Value.Date));
                }

                if (whereConditions.Count > 0)
                {
                    sql += " AND " + string.Join(" AND ", whereConditions);
                }

                sql += " ORDER BY P.Id DESC";

                return banco.ExecutarConsulta(sql, parameters.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar relatório de manutenção", ex);
                return new DataTable();
            }
        }
        public DataTable RelatorioManutencao(string procurar = "", int? idPatrimonio = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            try
            {
                string sql = @"SELECT P.Id AS IdPatrimonio,
                        P.Patrimonio,
                        M.Data_Reparo AS DataReparo,
                        M.Descricao,
                        M.Profissional,
                        M.Valor_Conserto as ValorConserto
                        FROM Patrimonios P
                        INNER JOIN Manutencao M ON P.Id = M.PatrimonioId
                        WHERE 1 = 1";

                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (idPatrimonio.HasValue)
                {
                    whereConditions.Add("P.Id = @idPatrimonio");
                    parameters.Add(new SqlParameter("@idPatrimonio", idPatrimonio.Value));
                }
                if (!string.IsNullOrEmpty(procurar))
                {
                    whereConditions.Add("M.Profissional LIKE @procurar");
                    parameters.Add(new SqlParameter("@procurar", $"%{procurar}%"));
                }

                if (dataInicial.HasValue)
                {
                    whereConditions.Add("M.Data_Reparo >= @dataInicial");
                    parameters.Add(new SqlParameter("@dataInicial", dataInicial.Value.Date));
                }

                if (dataFinal.HasValue)
                {
                    // Define a data final para o final do dia selecionado
                    dataFinal = dataFinal?.Date.AddDays(1).AddTicks(-1);

                    whereConditions.Add("M.Data_Reparo <= @dataFinal");
                    parameters.Add(new SqlParameter("@dataFinal", dataFinal.Value));
                }

                if (whereConditions.Count > 0)
                {
                    sql += " AND " + string.Join(" AND ", whereConditions);
                }

                sql += " ORDER BY P.Id DESC";

                return banco.ExecutarConsulta(sql, parameters.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar relatório de manutenção", ex);
                return new DataTable();
            }
        }

    }
}
