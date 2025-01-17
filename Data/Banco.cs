using System.Data;
using System.Data.SqlClient;


internal class Banco
{
    private static string connectionString = @"Data Source=172.16.10.169;Initial Catalog=db_controle;Persist Security Info=True;User ID=sa;Password=SanmaMacaco,#21";

    public SqlConnection Abrir()
    {
        try
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            return cnn;
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Ocorreu um erro SQL ao abrir a conexão. Detalhes: " + ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado ao abrir a conexão. Detalhes: " + ex.Message);
            return null;
        }
    }

    public void Fechar(SqlConnection connection)
    {
        try
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Ocorreu um erro SQL ao fechar a conexão. Detalhes: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado ao fechar a conexão. Detalhes: " + ex.Message);
        }
    }

    public void ExecutarComando(string sql, SqlParameter[] parameters)
    {
        using (SqlConnection connection = Abrir())
        {

            try
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao executar o comando no banco de dados: " + ex.Message);
            }
        }
    }
    public object ExecutarComandoScalar(string sql, SqlParameter[] parameters)
    {
        object result = null;

        using (SqlConnection connection = Abrir())
        {
            try
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao executar o comando no banco de dados: " + ex.Message);
            }
        }

        return result;
    }



    public DataTable ExecutarConsulta(string sql, SqlParameter[] parameters)
    {
        DataTable dataTable = new DataTable();

        using (SqlConnection connection = Abrir())
        {

            try
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao executar a consulta no banco de dados: " + ex.Message);
            }
        }

        return dataTable;
    }
}
