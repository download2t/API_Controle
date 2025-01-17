using System.Data.SqlClient;
using System.Data;

namespace API_CONTROLE.Entities
{
    public class PatrimoniosET
    {
        private Banco banco = new Banco();
       SubCategoriasET subaCTLCategorias = new SubCategoriasET();
        SetoresET aCTLSetores;

        public byte[] GetFoto(string caminhoFoto)
        {
            try
            {
                byte[] foto;
                using (var stream = new FileStream(caminhoFoto, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        foto = reader.ReadBytes((int)stream.Length);
                    }
                }
                return foto;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;//a exeção é para ser ignorada.
            }
        }
        public void AdicionarPatrimonio(Patrimonios patrimonio)
        {

            byte[] foto = GetFoto(patrimonio.CaminhoFoto);
            try
            {
                string sql = "INSERT INTO Patrimonios (Patrimonio, Descricao, Valor, SetorId, SubcategoriaId, Foto) " +
                             "VALUES (@Patrimonio, @Descricao, @Valor, @SetorId, @SubcategoriaId, @foto)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Patrimonio", patrimonio.Patrimonio),
                    new SqlParameter("@Descricao", patrimonio.Descricao),
                    new SqlParameter("@Valor", patrimonio.Valor),
                    new SqlParameter("@SetorId", patrimonio.Setor.Id),
                    new SqlParameter("@SubcategoriaId", patrimonio.Subcategoria.Id),
                };
                if (foto != null)
                {
                    // Adicione o parâmetro @Foto apenas quando houver uma foto
                    SqlParameter paramFoto = new SqlParameter("@Foto", SqlDbType.Image, foto.Length);
                    paramFoto.Value = foto;
                    parametros = parametros.Concat(new[] { paramFoto }).ToArray();
                }
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar patrimônio", ex);
            }
        }
        public void AtualizarPatrimonio(Patrimonios patrimonio)
        {
            byte[] foto = GetFoto(patrimonio.CaminhoFoto);
            try
            {
                string sql = "UPDATE Patrimonios " +
                             "SET Patrimonio = @Patrimonio, Descricao = @Descricao, Valor = @Valor, " +
                             "SetorId = @SetorId, SubcategoriaId = @SubcategoriaId, Foto = @Foto " +
                             "WHERE Id = @Id";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Patrimonio", patrimonio.Patrimonio),
                    new SqlParameter("@Descricao", patrimonio.Descricao),
                    new SqlParameter("@Valor", patrimonio.Valor),
                    new SqlParameter("@SetorId", patrimonio.Setor.Id),
                    new SqlParameter("@SubcategoriaId", patrimonio.Subcategoria.Id),
                    new SqlParameter("@Id", patrimonio.Id)
                };

                if (foto != null)
                {
                    // Adicione o parâmetro @Foto apenas quando houver uma foto
                    SqlParameter paramFoto = new SqlParameter("@Foto", SqlDbType.Image, foto.Length);
                    paramFoto.Value = foto;
                    parametros = parametros.Concat(new[] { paramFoto }).ToArray();
                }

                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar patrimônio", ex);
            }
        }
        public void AtualizarPatrimonioSemFoto(Patrimonios patrimonio)
        {
            try
            {
                string sql = "UPDATE Patrimonios " +
                             "SET Patrimonio = @Patrimonio, Descricao = @Descricao, Valor = @Valor, " +
                             "SetorId = @SetorId, SubcategoriaId = @SubcategoriaId " +
                             "WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Patrimonio", patrimonio.Patrimonio),
                    new SqlParameter("@Descricao", patrimonio.Descricao),
                    new SqlParameter("@Valor", patrimonio.Valor),
                    new SqlParameter("@SetorId", patrimonio.Setor.Id),
                    new SqlParameter("@SubcategoriaId", patrimonio.Subcategoria.Id),
                    new SqlParameter("@Id", patrimonio.Id)
                };
                banco.ExecutarComando(sql, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar patrimônio", ex);
            }
        }
        public bool ExcluirPatrimonio(int patrimonioId)
        {
            try
            {
                string sql = "DELETE FROM Patrimonios WHERE Id = @Id";
                SqlParameter[] parametros = { new SqlParameter("@Id", patrimonioId) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorne true para indicar sucesso
            }
            catch (SqlException ex)
            {
                    Console.WriteLine("Erro ao excluir patrimônio", ex);
                return false; // Retorne false para indicar falha
            }
            catch (Exception ex)
            {
                // Trate outras exceções genéricas, se aplicável
                Console.WriteLine("Erro ao excluir patrimônio", ex);
                return false; // Retorne false para indicar falha
            }
        }
        public Patrimonios BuscarPatrimonioPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Patrimonios WHERE Id = @Id AND Baixa = 'NAO'";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });
                aCTLSetores = new SetoresET();
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    int setorId = Convert.ToInt32(row["SetorId"]);
                    Setores setor = aCTLSetores.BuscarSetorPorId(setorId);

                    int subcategoriaId = Convert.ToInt32(row["SubcategoriaId"]);
                    Subcategoria subcategoria = subaCTLCategorias.BuscarSubcategoriaPorId(subcategoriaId);

                    byte[] foto = row["Foto"] as byte[]; // Trate possíveis valores nulos

                    return new Patrimonios
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Patrimonio = row["Patrimonio"].ToString(),
                        Descricao = row["Descricao"].ToString(),
                        Valor = Convert.ToDecimal(row["Valor"]),
                        Setor = setor,
                        Subcategoria = subcategoria,
                        Foto = foto, // Atribua o valor tratado
                        Baixa = row["Baixa"].ToString(),
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar patrimônio por ID", ex);
                return null;
            }
        }
        public List<Patrimonios> ListarPatrimonios()
        {
            try
            {
                string sql = "SELECT * FROM Patrimonios WHERE Baixa = 'NAO' Order By Id Desc";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                aCTLSetores = new SetoresET();
                subaCTLCategorias = new SubCategoriasET();
                List<Patrimonios> patrimonios = new List<Patrimonios>();

                foreach (DataRow row in dataTable.Rows)
                {
                    int setorId = Convert.ToInt32(row["SetorId"]);
                    Setores setor = aCTLSetores.BuscarSetorPorId(setorId);

                    int subcategoriaId = Convert.ToInt32(row["SubcategoriaId"]);
                    Subcategoria subcategoria = subaCTLCategorias.BuscarSubcategoriaPorId(subcategoriaId);

                    byte[] fotoBytes = row["Foto"] as byte[]; // Obtém os bytes da foto

                    patrimonios.Add(new Patrimonios
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Patrimonio = row["Patrimonio"].ToString(),
                        Descricao = row["Descricao"].ToString(),
                        Valor = Convert.ToDecimal(row["Valor"]),
                        Setor = setor,
                        Subcategoria = subcategoria,
                        Foto = fotoBytes,
                        Baixa = row["Baixa"].ToString(),
                    });
                }

                return patrimonios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar patrimônios", ex);
                return new List<Patrimonios>();
            }
        }
        public List<Patrimonios> PesquisarPatrimoniosPorCriterio(string criterio, string valorPesquisa)
        {
            List<Patrimonios> patrimoniosEncontrados = new List<Patrimonios>();

            try
            {
                using (SqlConnection connection = banco.Abrir())
                {
                    string query = string.Empty;
                    SqlCommand command = new SqlCommand();

                    if (criterio == "ID" && int.TryParse(valorPesquisa, out int id))
                    {
                        query = "SELECT * FROM Patrimonios WHERE Id = @ValorPesquisa AND Baixa = 'NAO'";
                        command.Parameters.AddWithValue("@ValorPesquisa", id);
                    }
                    else if (criterio == "Setor")
                    {
                        query = "SELECT * FROM Patrimonios WHERE SetorId IN (SELECT Id FROM Setores WHERE Setor LIKE @ValorPesquisa) AND Baixa = 'NAO'";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }
                    else if (criterio == "Categoria")
                    {
                        query = "SELECT * FROM Patrimonios WHERE SubcategoriaId IN (SELECT Id FROM SubCategoria WHERE CategoriaId IN (SELECT Id FROM Categorias WHERE Nome LIKE @ValorPesquisa)) AND Baixa = 'NAO'";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }
                    else if (criterio == "SubCategoria")
                    {
                        query = "SELECT * FROM Patrimonios WHERE SubcategoriaId IN (SELECT Id FROM SubCategoria WHERE Nome LIKE @ValorPesquisa) AND Baixa = 'NAO'";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }
                    else if (criterio == "Patrimonio")
                    {
                        query = "SELECT * FROM Patrimonios WHERE Patrimonio LIKE @ValorPesquisa AND Baixa = 'NAO'";
                        command.Parameters.AddWithValue("@ValorPesquisa", "%" + valorPesquisa + "%");
                    }

                    command.CommandText = query;
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patrimonios patrimonio = new Patrimonios
                            {
                                Id = (int)reader["Id"],
                                Patrimonio = (string)reader["Patrimonio"],
                                Descricao = (string)reader["Descricao"],
                                Valor = (decimal)reader["Valor"],
                                Setor = aCTLSetores.BuscarSetorPorId((int)reader["SetorId"]),
                                Subcategoria = subaCTLCategorias.BuscarSubcategoriaPorId((int)reader["SubcategoriaId"]),
                                Foto = reader["Foto"] as byte[],
                                Baixa = (string)reader["Baixa"],
                            };
                            patrimoniosEncontrados.Add(patrimonio);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Pesquisar patrimônios por critério", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pesquisar patrimônios por critério", ex);
            }

            return patrimoniosEncontrados;
        }
        public DataTable Relatorios2(string categoria = null, string setor = null, string subcategoria = null, bool? status = null)
        {
            try
            {
                string sql = @"SELECT 
                            P.Patrimonio,
                            C.Nome AS Categoria,
                            SC.Nome AS Subcategoria,
                            S.Setor,
                            P.Descricao,
                            P.Valor
                       FROM 
                            Patrimonios P
                       INNER JOIN 
                            SubCategoria SC ON P.SubcategoriaId = SC.Id
                       INNER JOIN 
                            Categorias C ON SC.CategoriaId = C.Id
                       INNER JOIN 
                            Setores S ON P.SetorId = S.Id
                       WHERE 
                            P.Baixa = 'NAO'";

                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(categoria))
                {
                    whereConditions.Add("C.Nome = @categoria");
                    parameters.Add(new SqlParameter("@categoria", categoria));
                }

                if (!string.IsNullOrEmpty(setor))
                {
                    whereConditions.Add("S.Setor = @setor");
                    parameters.Add(new SqlParameter("@setor", setor));
                }

                if (!string.IsNullOrEmpty(subcategoria))
                {
                    whereConditions.Add("SC.Nome = @subcategoria");
                    parameters.Add(new SqlParameter("@subcategoria", subcategoria));
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
                Console.WriteLine("Erro ao listar patrimônios", ex);
                return new DataTable();
            }
        }

        public DataTable Relatorios(string categoria = null, string setor = null, string subcategoria = null, bool? status = null)
        {
            try
            {
                string sql = @"SELECT 
                         P.Patrimonio,
                         C.Nome AS Categoria,
                         SC.Nome AS Subcategoria,
                         S.Setor,
                         P.Descricao,
                         P.Valor
                     FROM 
                         Patrimonios P
                     INNER JOIN 
                         SubCategoria SC ON P.SubcategoriaId = SC.Id
                     INNER JOIN 
                         Categorias C ON SC.CategoriaId = C.Id
                     INNER JOIN 
                         Setores S ON P.SetorId = S.Id
                     WHERE 1 = 1";

                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (status != null)
                {
                    if (status == false)
                    {
                        sql += " AND P.Baixa = 'SIM'";
                    }
                    else
                    {
                        sql += " AND P.Baixa = 'NAO'";
                    }
                }

                if (!string.IsNullOrEmpty(categoria))
                {
                    whereConditions.Add("C.Nome = @categoria");
                    parameters.Add(new SqlParameter("@categoria", categoria));
                }

                if (!string.IsNullOrEmpty(setor))
                {
                    whereConditions.Add("S.Setor = @setor");
                    parameters.Add(new SqlParameter("@setor", setor));
                }

                if (!string.IsNullOrEmpty(subcategoria))
                {
                    whereConditions.Add("SC.Nome = @subcategoria");
                    parameters.Add(new SqlParameter("@subcategoria", subcategoria));
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
                Console.WriteLine("Erro ao listar patrimônios", ex);
                return new DataTable();
            }
        }

    }
}
