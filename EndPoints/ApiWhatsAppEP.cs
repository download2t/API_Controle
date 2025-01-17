using API_CONTROLE.Entities;
using API_CONTROLE.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace web_controle.EndPoints
{
    public static class ApiWhatsAppEP
    {
        private static readonly ApiWhatsAppET apiWhatsAppET = new ApiWhatsAppET();

        // Enviar Mensagem
        // POST /EnviarMensagem
        [HttpPost("/EnviarMensagem")]
        [SwaggerOperation(Summary = "Enviar mensagem via WhatsApp", Description = "Envia uma mensagem para um número de telefone via WhatsApp.")]
        public static async Task<IActionResult> EnviarMensagem([FromBody] ApiWhatsAppRequest request)
        {
            if (request == null || request.ApiWhatsApp == null || string.IsNullOrEmpty(request.Message))
            {
                return new BadRequestObjectResult("Dados inválidos. Verifique se o ApiWhatsApp e a mensagem estão corretamente preenchidos.");
            }

            try
            {
                bool sucesso = await apiWhatsAppET.EnviarMensagem(request.ApiWhatsApp, request.Message);

                return sucesso ? new OkResult() : new BadRequestResult();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

    }

    public class ApiWhatsAppRequest
    {
        public ApiWhatsApp ApiWhatsApp { get; set; }
        public string Message { get; set; }
    }
}
