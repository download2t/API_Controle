using System.Data.SqlClient;
using System.Data;
using API_Loja.Entities;

namespace API_CONTROLE.Entities
{
    public class FuncionariosET
    {

        private Banco banco = new Banco();
        SetoresET aCTLSetores = new SetoresET();
        CargosET oCTLCargo = new CargosET();


        public void AdicionarFuncionario(Funcionario funcionario)
        {
            try
            {
                string sql = "INSERT INTO Funcionarios (Nome, Cpf, SetorId, CargoId, SalBruto, Ativo) " +
                             "VALUES (@Nome, @Cpf, @SetorId, @CargoId, @SalBruto, @Ativo)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", funcionario.Nome),
                    new SqlParameter("@Cpf", funcionario.Cpf),
                    new SqlParameter("@SetorId", funcionario.Setor.Id),
                    new SqlParameter("@CargoId", funcionario.Cargo.Id),
                    new SqlParameter("@SalBruto", funcionario.SalBruto),
                    new SqlParameter("@Ativo", funcionario.Ativo)
                };
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar funcionário", ex);
            }
        }

        public void AtualizarFuncionario(Funcionario funcionario)
        {
            try
            {
                string sql = "UPDATE Funcionarios SET Nome = @Nome, Cpf = @Cpf, SetorId = @SetorId, " +
                             "CargoId = @CargoId, SalBruto = @SalBruto, Ativo = @Ativo WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", funcionario.Nome),
                    new SqlParameter("@Cpf", funcionario.Cpf),
                    new SqlParameter("@SetorId", funcionario.Setor.Id),
                    new SqlParameter("@CargoId", funcionario.Cargo.Id),
                    new SqlParameter("@SalBruto", funcionario.SalBruto),
                    new SqlParameter("@Ativo", funcionario.Ativo),
                    new SqlParameter("@Id", funcionario.Id)
                };
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar funcionário", ex);
            }
        }

        public bool ExcluirFuncionario(int funcionarioId)
        {
            try
            {
                string sql = "DELETE FROM Funcionarios WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", funcionarioId);
                banco.ExecutarComando(sql, new[] { parametro });
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Erro ao excluir funcionário", ex);
                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir funcionário", ex);
                return false; // Retorne false para indicar falha
            }
        }

        public Funcionario BuscarFuncionarioPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Funcionarios WHERE Id = @Id";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateFuncionarioFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar funcionário por ID", ex);
                return null;
            }
        }
        public List<Funcionario> ListarFuncionarios(string usuarioAtivo)
        {
            try
            {
                string statusAtivo = usuarioAtivo == "Sim" ? "S" : "N"; // Convertendo para o formato do banco de dados

                string sql = "SELECT * FROM Funcionarios WHERE Ativo = @StatusAtivo ORDER BY Id DESC";
                SqlParameter parametroStatusAtivo = new SqlParameter("@StatusAtivo", statusAtivo);

                DataTable dataTable = banco.ExecutarConsulta(sql, new[] { parametroStatusAtivo });

                List<Funcionario> funcionarios = new List<Funcionario>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Funcionario funcionario = CreateFuncionarioFromDataRow(row);
                    funcionarios.Add(funcionario);
                }

                return funcionarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar funcionários", ex);
                return new List<Funcionario>();
            }
        }


        private Funcionario CreateFuncionarioFromDataRow(DataRow row)
        {
            int setorId = Convert.ToInt32(row["SetorId"]);
            Setores setor = aCTLSetores.BuscarSetorPorId(setorId);

            int cargoId = Convert.ToInt32(row["CargoId"]);
            Cargo cargo = oCTLCargo.BuscarCargoPorId(cargoId);

            return new Funcionario
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                Setor = setor,
                Cargo = cargo,
                Cpf = row["Cpf"].ToString(),
                Telefone = row["Telefone"].ToString(),
                SalBruto = row["SalBruto"] != DBNull.Value ? Convert.ToDecimal(row["SalBruto"]) : (decimal?)null,
                Ativo = Convert.ToChar(row["Ativo"])
            };
        }



        private List<Funcionario> CreateFuncionariosListFromDataTable(DataTable dataTable)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (DataRow row in dataTable.Rows)
            {
                funcionarios.Add(CreateFuncionarioFromDataRow(row));
            }
            return funcionarios;
        }

        public List<Funcionario> PesquisarFuncionariosPorCriterio(string criterio, string valorPesquisa)
        {
            try
            {
                string query = string.Empty;
                SqlParameter parametro;

                if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                {
                    query = "SELECT * FROM Funcionarios WHERE Id = @Id";
                    parametro = new SqlParameter("@Id", id);
                }
                else if (criterio == "Nome")
                {
                    query = "SELECT * FROM Funcionarios WHERE Nome LIKE @ValorPesquisa";
                    parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                }
                else if (criterio == "Cpf")
                {
                    query = "SELECT * FROM Funcionarios WHERE Cpf LIKE @ValorPesquisa";
                    parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                }
                else
                {
                    return new List<Funcionario>();
                }

                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });
                return CreateFuncionariosListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao pesquisar funcionários por {criterio.ToLower()}", ex);
                return new List<Funcionario>();
            }
        }

    }
}
