using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class PermissaoMenuET
    {
        private Banco banco = new Banco();
        UsuariosET usuarioController = new UsuariosET();

        public void SalvarUsuarioEPermissao(PermissaoMenu permissaoMenu, Usuarios usuario)
        {
            using (SqlConnection connection = banco.Abrir())
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string verificarExistenciaSql = "SELECT COUNT(*) FROM PermissaoMenu WHERE UsuarioId = @UsuarioId AND MenuOpcaoId = @MenuOpcaoId";
                    SqlParameter[] verificarExistenciaParametros =
                    {
                        new SqlParameter("@UsuarioId", permissaoMenu.Usuario.Id),
                        new SqlParameter("@MenuOpcaoId", permissaoMenu.Opcao.Id)
                    };

                    int count = (int)banco.ExecutarConsulta(verificarExistenciaSql, verificarExistenciaParametros).Rows[0][0];

                    if (count > 0)
                    {
                        // Atualizar permissão de menu
                        string permissaoSql = "UPDATE PermissaoMenu " +
                                              "SET PodeAdicionar = @PodeAdicionar, PodeAlterar = @PodeAlterar, " +
                                              "PodeExcluir = @PodeExcluir, PodeConsultar = @PodeConsultar " +
                                              "WHERE UsuarioId = @UsuarioId AND MenuOpcaoId = @MenuOpcaoId";

                        SqlParameter[] permissaoParametros =
                        {
                            new SqlParameter("@PodeAdicionar", permissaoMenu.PodeAdicionar),
                            new SqlParameter("@PodeAlterar", permissaoMenu.PodeAlterar),
                            new SqlParameter("@PodeExcluir", permissaoMenu.PodeExcluir),
                            new SqlParameter("@PodeConsultar", permissaoMenu.PodeConsultar),
                            new SqlParameter("@UsuarioId", permissaoMenu.Usuario.Id),
                            new SqlParameter("@MenuOpcaoId", permissaoMenu.Opcao.Id)
                        };

                        banco.ExecutarComando(permissaoSql, permissaoParametros);
                    }
                    else
                    {
                        // Inserir nova permissão de menu
                        string novaPermissaoSql = "INSERT INTO PermissaoMenu (UsuarioId, MenuOpcaoId, PodeAdicionar, PodeAlterar, PodeExcluir, PodeConsultar) " +
                                                  "VALUES (@UsuarioId, @MenuOpcaoId, @PodeAdicionar, @PodeAlterar, @PodeExcluir, @PodeConsultar)";

                        SqlParameter[] novaPermissaoParametros =
                        {
                            new SqlParameter("@UsuarioId", permissaoMenu.Usuario.Id),
                            new SqlParameter("@MenuOpcaoId", permissaoMenu.Opcao.Id),
                            new SqlParameter("@PodeAdicionar", permissaoMenu.PodeAdicionar),
                            new SqlParameter("@PodeAlterar", permissaoMenu.PodeAlterar),
                            new SqlParameter("@PodeExcluir", permissaoMenu.PodeExcluir),
                            new SqlParameter("@PodeConsultar", permissaoMenu.PodeConsultar)
                        };

                        banco.ExecutarComando(novaPermissaoSql, novaPermissaoParametros);
                    }

                    // Atualizar usuário
                    string usuarioSql = "UPDATE Usuarios SET Nome = @Nome, Sobrenome = @Sobrenome, " +
                                        "Email = @Email, Senha = @Senha, " +
                                        "Usuario = @Usuario, Perfil = @Perfil, " +
                                        "Status = @Status, DataNascimento = @DataNascimento WHERE Id = @Id";

                    SqlParameter[] usuarioParametros =
                    {
                        new SqlParameter("@Nome", usuario.Nome),
                        new SqlParameter("@Sobrenome", usuario.Sobrenome),
                        new SqlParameter("@Email", usuario.Email),
                        new SqlParameter("@Senha", usuario.Senha),
                        new SqlParameter("@Usuario", usuario.Usuario),
                        new SqlParameter("@Perfil", usuario.Perfil),
                        new SqlParameter("@Status", usuario.Status),
                        new SqlParameter("@DataNascimento", usuario.DataNascimento),
                        new SqlParameter("@Id", usuario.Id)
                    };

                    banco.ExecutarComando(usuarioSql, usuarioParametros);

                    // Se todas as operações foram bem-sucedidas, commit na transação
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Se houver qualquer exceção, rollback na transação para desfazer as alterações
                    transaction.Rollback();
                    Console.WriteLine("Ocorreu um erro ao salvar as alterações: " + ex.Message, "Erro");
                }
            }
        }

        public static bool OpcaoLiberada(string opcao, List<PermissaoMenu> usuarioAcessos, Func<PermissaoMenu, bool> propriedade)
        {
            foreach (var item in usuarioAcessos)
            {
                if (item.Opcao.Nome == opcao)
                {
                    return propriedade(item);
                }
            }
            return false;
        }

        // Métodos específicos chamando o método genérico para verificar diferentes propriedades booleanas
        public bool OpcaoLiberadaAdicionar(string opcao, List<PermissaoMenu> usuarioAcessos)
        {
            return OpcaoLiberada(opcao, usuarioAcessos, x => x.PodeAdicionar);
        }

        public bool OpcaoLiberadaAlterar(string opcao, List<PermissaoMenu> usuarioAcessos)
        {
            return OpcaoLiberada(opcao, usuarioAcessos, x => x.PodeAlterar);
        }

        public bool OpcaoLiberadaExcluir(string opcao, List<PermissaoMenu> usuarioAcessos)
        {
            return OpcaoLiberada(opcao, usuarioAcessos, x => x.PodeExcluir);
        }

        public bool OpcaoLiberadaConsultar(string opcao, List<PermissaoMenu> usuarioAcessos)
        {
            return OpcaoLiberada(opcao, usuarioAcessos, x => x.PodeConsultar);
        }


        public void AdicionarPermissaoMenu(PermissaoMenu permissaoMenu)
        {
            try
            {
                string sql = "INSERT INTO PermissaoMenu (UsuarioId, MenuOpcaoId, PodeAdicionar, PodeAlterar, PodeExcluir, PodeConsultar) " +
                             "VALUES (@UsuarioId, @MenuOpcaoId, @PodeAdicionar, @PodeAlterar, @PodeExcluir, @PodeConsultar)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@UsuarioId", permissaoMenu.Usuario.Id),
                    new SqlParameter("@MenuOpcaoId", permissaoMenu.Opcao.Id),
                    new SqlParameter("@PodeAdicionar", permissaoMenu.PodeAdicionar),
                    new SqlParameter("@PodeAlterar", permissaoMenu.PodeAlterar),
                    new SqlParameter("@PodeExcluir", permissaoMenu.PodeExcluir),
                    new SqlParameter("@PodeConsultar", permissaoMenu.PodeConsultar)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar permissão de menu", ex);
            }
        }
        public void AtualizarPermissaoMenu(PermissaoMenu permissaoMenu)
        {
            try
            {
                string sql = "UPDATE PermissaoMenu " +
                             "SET PodeAdicionar = @PodeAdicionar, PodeAlterar = @PodeAlterar, " +
                             "PodeExcluir = @PodeExcluir, PodeConsultar = @PodeConsultar " +
                             "WHERE UsuarioId = @UsuarioId AND MenuOpcaoId = @MenuOpcaoId";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@UsuarioId", permissaoMenu.Usuario.Id),
                    new SqlParameter("@MenuOpcaoId", permissaoMenu.Opcao.Id),
                    new SqlParameter("@PodeAdicionar", permissaoMenu.PodeAdicionar),
                    new SqlParameter("@PodeAlterar", permissaoMenu.PodeAlterar),
                    new SqlParameter("@PodeExcluir", permissaoMenu.PodeExcluir),
                    new SqlParameter("@PodeConsultar", permissaoMenu.PodeConsultar)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar permissão de menu", ex);
            }
        }
        public void RemoverPermissaoMenu(int usuarioId, int menuOpcaoId)// Não está sendo utilizado.
        {
            try
            {
                string sql = "DELETE FROM PermissaoMenu WHERE UsuarioId = @UsuarioId AND MenuOpcaoId = @MenuOpcaoId";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@MenuOpcaoId", menuOpcaoId)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao remover permissão de menu", ex);
            }
        }
        public PermissaoMenu ObterPermissaoMenu(int usuarioId, int menuOpcaoId)
        {
            try
            {
                string sql = "SELECT * FROM PermissaoMenu WHERE UsuarioId = @UsuarioId AND MenuOpcaoId = @MenuOpcaoId";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@MenuOpcaoId", menuOpcaoId)
                };

                DataTable dataTable = banco.ExecutarConsulta(sql, parametros);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return new PermissaoMenu
                    {
                        Usuario = new Usuarios { Id = Convert.ToInt32(row["UsuarioId"]) },
                        Opcao = new Opcoes { Id = Convert.ToInt32(row["MenuOpcaoId"]) },
                        PodeAdicionar = Convert.ToBoolean(row["PodeAdicionar"]),
                        PodeAlterar = Convert.ToBoolean(row["PodeAlterar"]),
                        PodeExcluir = Convert.ToBoolean(row["PodeExcluir"]),
                        PodeConsultar = Convert.ToBoolean(row["PodeConsultar"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter permissão de menu", ex);
                return null;
            }
        }

        public List<PermissaoMenu> ObterPermissoesPorUsuario(int usuarioId)
        {
            List<PermissaoMenu> listaPermissoes = new List<PermissaoMenu>();

            try
            {
                string sql = @"SELECT mo.id, mo.nome AS nomeOpcao, mo.descricao AS descricaoOpcao, mo.nivel, 
               pm.PodeAdicionar, pm.PodeAlterar, pm.PodeExcluir, pm.PodeConsultar
               FROM MenuOpcoes mo
               LEFT JOIN PermissaoMenu pm ON mo.id = pm.MenuOpcaoId AND pm.UsuarioId = @UsuarioId";

                SqlParameter parametro = new SqlParameter("@UsuarioId", usuarioId);

                DataTable dataTable = banco.ExecutarConsulta(sql, new[] { parametro });

                foreach (DataRow row in dataTable.Rows)
                {
                    PermissaoMenu permissao = new PermissaoMenu
                    {
                        Usuario = new Usuarios { Id = usuarioId },
                        Opcao = new Opcoes
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Nome = Convert.ToString(row["nomeOpcao"]),
                            Descricao = Convert.ToString(row["descricaoOpcao"]),
                            Nivel = Convert.ToByte(row["nivel"])
                        },
                        PodeAdicionar = row["PodeAdicionar"] != DBNull.Value && Convert.ToBoolean(row["PodeAdicionar"]),
                        PodeAlterar = row["PodeAlterar"] != DBNull.Value && Convert.ToBoolean(row["PodeAlterar"]),
                        PodeExcluir = row["PodeExcluir"] != DBNull.Value && Convert.ToBoolean(row["PodeExcluir"]),
                        PodeConsultar = row["PodeConsultar"] != DBNull.Value && Convert.ToBoolean(row["PodeConsultar"])
                    };

                    listaPermissoes.Add(permissao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter permissões do usuário", ex);
            }

            return listaPermissoes;
        }
        public List<Opcoes> ObterOpcoesMenuExceto(string tipo)
        {
            List<Opcoes> listaOpcoes = new List<Opcoes>();

            try
            {
                string sql = "";
                if (tipo == "CHEFE")
                {
                    sql = @"SELECT id, nome, descricao, nivel
                               FROM MenuOpcoes
                               WHERE nome NOT IN ('Sistema / Configurar Menu', 'Sistema / Usuários do Sistema', 'Gerenciar Senhas / Senhas Importantes')";
                }
                else if (tipo == "USUARIO")
                {
                    sql = @"SELECT id, nome, descricao, nivel
                               FROM MenuOpcoes
                               WHERE nome NOT IN ('Sistema / Configurar Menu', 'Sistema / Usuários do Sistema', 'Gerenciar Senhas / Senhas Importantes')";
                }

                DataTable dataTable = banco.ExecutarConsulta(sql, null);

                foreach (DataRow row in dataTable.Rows)
                {
                    Opcoes opcao = new Opcoes
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nome = Convert.ToString(row["nome"]),
                        Descricao = Convert.ToString(row["descricao"]),
                        Nivel = Convert.ToByte(row["nivel"])
                    };

                    listaOpcoes.Add(opcao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter opções de menu", ex);
            }

            return listaOpcoes;
        }

    }
}
