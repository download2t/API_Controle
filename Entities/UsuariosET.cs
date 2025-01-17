using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace API_CONTROLE.Entities
{
    public class UsuariosET
    {
        private Banco banco = new Banco();

        public void AdicionarUsuario(Usuarios usuario)
        {// colocar as permissões do usuario como chefe visualisação ou usuário, dar permissões de menu ao adicionar novo usuário
            try
            {
                string sql = "INSERT INTO Usuarios (Nome, Sobrenome, Email, Senha, Usuario, Perfil, Status, DataCadastro, DataNascimento) " +
                             "VALUES (@Nome, @Sobrenome, @Email, @Senha, @Usuario, @Perfil, @Status, @DataCadastro, @DataNascimento)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", usuario.Nome),
                    new SqlParameter("@Sobrenome", usuario.Sobrenome),
                    new SqlParameter("@Email", usuario.Email),
                    new SqlParameter("@Senha", usuario.Senha),
                    new SqlParameter("@Usuario", usuario.Usuario),
                    new SqlParameter("@Perfil", usuario.Perfil),
                    new SqlParameter("@Status", usuario.Status),
                    new SqlParameter("@DataCadastro", usuario.DataCadastro),
                    new SqlParameter("@DataNascimento", usuario.DataNascimento)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("adicionar o usuário", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("adicionar o usuário", ex);
            }
        }
        public void AtualizarUsuario(Usuarios usuario)
        {
            try
            {
                string sql = "UPDATE Usuarios SET Nome = @Nome, Sobrenome = @Sobrenome, " +
                             "Email = @Email, Senha = @Senha, " +
                             "Usuario = @Usuario, Perfil = @Perfil, " +
                             "Status = @Status, DataNascimento = @DataNascimento WHERE Id = @Id";

                SqlParameter[] parametros =
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

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("atualizar o usuário", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("atualizar o usuário", ex);
            }
        }
        public void AtualizarUsuarioSemSenha(Usuarios usuario)
        {
            try
            {
                string sql = "UPDATE Usuarios SET Nome = @Nome, Sobrenome = @Sobrenome, " +
                             "Email = @Email,  " +
                             "Usuario = @Usuario, Perfil = @Perfil, " +
                             "Status = @Status, DataNascimento = @DataNascimento WHERE Id = @Id";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", usuario.Nome),
                    new SqlParameter("@Sobrenome", usuario.Sobrenome),
                    new SqlParameter("@Email", usuario.Email),
                    new SqlParameter("@Usuario", usuario.Usuario),
                    new SqlParameter("@Perfil", usuario.Perfil),
                    new SqlParameter("@Status", usuario.Status),
                    new SqlParameter("@DataNascimento", usuario.DataNascimento),
                    new SqlParameter("@Id", usuario.Id)
                };

                banco.ExecutarComando(sql, parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("atualizar o usuário sem senha", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("atualizar o usuário sem senha", ex);
            }
        }
        public bool AlterarSenha(Usuarios usuario)
        {
            using (SqlConnection connection = banco.Abrir())
            {
                string sql = "UPDATE Usuarios SET Senha = @Senha WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
                    command.Parameters.AddWithValue("@Id", usuario.Id);

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("alterar senha do usuário", ex);
                    }
                }
            }
            return false;
        }
        public bool ExcluirUsuario(int usuarioId)
        {
            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "DELETE FROM Usuarios WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", usuarioId);
                    command.ExecuteNonQuery();
                    return true; // Retorne true para indicar sucesso
                }
            }
            catch (SqlException ex)
            {
                    Console.WriteLine("Erro ao excluir o usuário", ex);
                
                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir o usuário", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Usuarios BuscarUsuarioPorId(int id)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Usuarios WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuarios
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Usuario = reader["Usuario"].ToString(),
                                Perfil = reader["Perfil"].ToString(),
                                Status = reader["Status"].ToString(),
                                DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"])
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("buscar o usuário por ID", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("buscar o usuário por ID", ex);
            }

            return usuario;
        }
        public Usuarios BuscarUsuarioPorNome(string nome)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Usuarios WHERE Usuario = @usuario"; // Corrigido para @usuario
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@usuario", nome); // Corrigido para @usuario

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuarios
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Usuario = reader["Usuario"].ToString(),
                                Perfil = reader["Perfil"].ToString(),
                                Status = reader["Status"].ToString(),
                                DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"])
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("buscar o usuário por ID", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("buscar o usuário por ID", ex);
            }

            return usuario;
        }
        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "SELECT * FROM Usuarios";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuarios usuario = new Usuarios
                        {
                            Id = (int)reader["Id"],
                            Nome = (string)reader["Nome"],
                            Sobrenome = (string)reader["Sobrenome"],
                            Email = (string)reader["Email"],
                            Senha = (string)reader["Senha"],
                            Usuario = (string)reader["Usuario"],
                            Perfil = (string)reader["Perfil"],
                            Status = (string)reader["Status"],
                            DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                            DataNascimento = Convert.ToDateTime(reader["DataNascimento"])
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("listar usuários", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("listar usuários", ex);
            }

            return usuarios;
        }
        public Usuarios AutenticarUsuario(string username, string password)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Usuarios WHERE Usuario = @Usuario AND Senha = @Senha AND Status = 'Ativo'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", username);
                    command.Parameters.AddWithValue("@Senha", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuarios
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Usuario = reader["Usuario"].ToString(),
                                Perfil = reader["Perfil"].ToString(),
                                Status = reader["Status"].ToString(),
                                DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"])
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("autenticar o usuário", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("autenticar o usuário", ex);
            }

            return usuario;
        }

        public List<Usuarios> PesquisarUsuariosPorCriterio(string criterio, string valorPesquisa)
        {
            List<Usuarios> usuariosEncontrados = new List<Usuarios>();

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = "SELECT * FROM Usuarios WHERE ";
                    SqlCommand command = new SqlCommand();

                    if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                    {
                        query += "Id = @ValorPesquisa";
                        command.Parameters.AddWithValue("@ValorPesquisa", id);
                    }
                    else if (criterio == "Nome")
                    {
                        query += "Nome LIKE @ValorPesquisa";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }
                    else if (criterio == "Email")
                    {
                        query += "Email LIKE @ValorPesquisa";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }
                    else
                    {
                        // Trate aqui o caso em que o critério não é reconhecido.
                        // Pode ser lançada uma exceção ou tratado de outra forma apropriada.
                    }

                    command.CommandText = query;
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuarios usuario = new Usuarios
                            {
                                Id = (int)reader["Id"],
                                Nome = (string)reader["Nome"],
                                Sobrenome = (string)reader["Sobrenome"],
                                Email = (string)reader["Email"],
                                Usuario = (string)reader["Usuario"],
                                DataNascimento = reader["DataNascimento"] as DateTime?,
                                Senha = (string)reader["Senha"],
                                Status = (string)reader["Status"],
                                Perfil = (string)reader["Perfil"],
                                DataCadastro = reader["DataCadastro"] as DateTime?
                            };

                            usuariosEncontrados.Add(usuario);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"pesquisar usuários por {criterio.ToLower()}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"pesquisar usuários por {criterio.ToLower()}", ex);
            }

            return usuariosEncontrados;
        }

        public static string CriptografarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = sha256.ComputeHash(bytes);
                string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return hashString;
            }
        }
        public bool VerificarAdministrador(int idUsuario)
        {
            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string sql = "SELECT COUNT(*) FROM Usuarios WHERE Id = @Id AND Perfil = 'Admin'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idUsuario);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("verificar se o usuário é administrador", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("verificar se o usuário é administrador", ex);
            }

            return false; // Em caso de erro ou se o usuário não for encontrado, assume-se que não é um administrador
        }

    }
}
