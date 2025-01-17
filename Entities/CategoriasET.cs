using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API_Loja.Repository;
using API_Loja.Entities;

namespace API_Loja.Entities
{
    public class CategoriaET
    {
        private Banco banco = new Banco();

        public bool AdicionarCategoria(Categoria categoria)
        {
            try
            {
                string sql = "INSERT INTO Categorias (Nome) VALUES (@Nome)";
                SqlParameter[] parametros = { new SqlParameter("@Nome", categoria.Nome) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar categoria: ", ex);
                return false; // Retorna false se ocorrer uma exceção
            }
        }

        public bool AtualizarCategoria(Categoria categoria)
        {
            try
            {
                string sql = "UPDATE Categorias SET Nome = @Nome WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", categoria.Nome),
                    new SqlParameter("@Id", categoria.Id)
                };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar categoria: ", ex);
                return false; // Retorna false se ocorrer uma exceção
            }
        }

        public bool ExcluirCategoria(int categoriaId)
        {
            try
            {
                string sql = "DELETE FROM Categorias WHERE Id = @Id";
                SqlParameter[] parametros = { new SqlParameter("@Id", categoriaId) };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir categoria: ", ex);
                return false; // Retorna false para indicar falha
            }
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM Categorias WHERE Id = @Id AND Senha IS NULL";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateCategoriaFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar categoria por ID: ", ex);
                return null;
            }
        }

        public List<Categoria> BuscarCategoriaPorNome(string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Categorias WHERE Nome LIKE @ValorPesquisa AND Senha IS NULL";
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                return CreateCategoriasListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar categorias por nome: ", ex);
                return new List<Categoria>();
            }
        }

        public List<Categoria> ListarCategorias()
        {
            try
            {
                string sql = "SELECT * FROM Categorias WHERE Senha IS NULL ORDER BY Id DESC";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);
                return CreateCategoriasListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar categorias: ", ex);
                return new List<Categoria>();
            }
        }

        public bool AdicionarCategoriaDeSenhas(Categoria categoria)
        {
            try
            {
                string sql = "INSERT INTO Categorias (Nome, Senha) VALUES (@Nome, @Senha)";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", categoria.Nome),
                    new SqlParameter("@Senha", categoria.Senha)
                };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar categoria com senha: ", ex);
                return false; // Retorna false se ocorrer uma exceção
            }
        }

        public bool AtualizarCategoriaDeSenha(Categoria categoria)
        {
            try
            {
                string sql = "UPDATE Categorias SET Nome = @Nome, Senha = @Senha WHERE Id = @Id";
                SqlParameter[] parametros =
                {
                    new SqlParameter("@Nome", categoria.Nome),
                    new SqlParameter("@Senha", categoria.Senha),
                    new SqlParameter("@Id", categoria.Id)
                };
                banco.ExecutarComando(sql, parametros);
                return true; // Retorna true se a operação for bem-sucedida
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar categoria com senha: ", ex);
                return false; // Retorna false se ocorrer uma exceção
            }
        }

        public List<Categoria> BuscarCategoriaPorNomeDeSenhas(string valorPesquisa)
        {
            try
            {
                string query = "SELECT * FROM Categorias WHERE Nome LIKE @ValorPesquisa AND Senha <> ''";
                SqlParameter parametro = new SqlParameter("@ValorPesquisa", "%" + valorPesquisa + "%");
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                return CreateCategoriasListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar categorias por nome com senha: ", ex);
                return new List<Categoria>();
            }
        }

        public List<Categoria> ListarCategoriasDeSenhas()
        {
            try
            {
                string sql = "SELECT * FROM Categorias WHERE Senha IS NOT NULL AND Senha <> '' ORDER BY Id DESC";
                DataTable dataTable = banco.ExecutarConsulta(sql, null);

                return CreateCategoriasListFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar categorias com senha: ", ex);
                return new List<Categoria>();
            }
        }

        public Categoria BuscarCategoriaPorIdDeSenhas(int id)
        {
            try
            {
                string query = "SELECT * FROM Categorias WHERE Id = @Id AND Senha <> ''";
                SqlParameter parametro = new SqlParameter("@Id", id);
                DataTable dataTable = banco.ExecutarConsulta(query, new[] { parametro });

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return CreateCategoriaFromDataRow(row);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar categoria com senha por ID: ", ex);
                return null;
            }
        }

        private Categoria CreateCategoriaFromDataRow(DataRow row)
        {
            return new Categoria
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                Senha = row["Senha"] != DBNull.Value,
            };
        }

        private List<Categoria> CreateCategoriasListFromDataTable(DataTable dataTable)
        {
            List<Categoria> categorias = new List<Categoria>();
            foreach (DataRow row in dataTable.Rows)
            {
                categorias.Add(CreateCategoriaFromDataRow(row));
            }
            return categorias;
        }
    }
}
