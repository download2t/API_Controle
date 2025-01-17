using API_CONTROLE.Repository;
using System.Text;
using System.Threading.Tasks;

namespace API_CONTROLE.Entities
{
    public class ApiWhatsAppET
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<bool> EnviarMensagem(ApiWhatsApp apiWhatsApp, string message)
        {
            try
            {
                // Criando o objeto de mensagem no formato JSON
                var content = new StringContent(
                    $"{{ \"chatId\": \"{apiWhatsApp.Telefone}\", \"contentType\": \"string\", \"content\": \"{message}\" }}",
                    Encoding.UTF8,
                    "application/json");

                // Configurando os headers
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("x-api-key", apiWhatsApp.ApiKey);

                // Enviando a mensagem via POST para a API do WhatsApp
                HttpResponseMessage response = await client.PostAsync(apiWhatsApp.ApiPath, content);

                // Verificando se a requisição foi bem-sucedida
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log de erro
                Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
                return false;
            }
        }
    }
}
