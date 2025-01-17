using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class SenhasEP
    {
        private static SenhasET senhasET = new SenhasET();

        [HttpPost("/AdicionarSenha")]
        [SwaggerOperation(Summary = "Adicionar senha", Description = "Adiciona uma nova senha.")]
        public static IActionResult AdicionarSenha([FromBody] Senhas senha)
        {
            try
            {
                senhasET.AdicionarSenha(senha);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar a senha: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarSenha")]
        [SwaggerOperation(Summary = "Atualizar senha", Description = "Atualiza uma senha existente.")]
        public static IActionResult AtualizarSenha([FromBody] Senhas senha)
        {
            try
            {
                senhasET.AtualizarSenha(senha);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar a senha: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirSenha/{senhaId}")]
        [SwaggerOperation(Summary = "Excluir senha", Description = "Remove uma senha pelo ID.")]
        public static IActionResult ExcluirSenha([FromRoute] int senhaId)
        {
            try
            {
                bool sucesso = senhasET.ExcluirSenha(senhaId);
                if (sucesso)
                {
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao excluir a senha: " + ex.Message);
            }
        }

        [HttpGet("/BuscarSenhaPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar senha por ID", Description = "Obtém uma senha pelo ID.")]
        public static IActionResult BuscarSenhaPorId([FromRoute] int id)
        {
            try
            {
                var senha = senhasET.BuscarSenhaPorId(id);
                if (senha != null)
                {
                    return new OkObjectResult(senha);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao buscar a senha por ID: " + ex.Message);
            }
        }

        [HttpGet("/ListarSenhas")]
        [SwaggerOperation(Summary = "Listar senhas", Description = "Obtém todas as senhas.")]
        public static IActionResult ListarSenhas()
        {
            try
            {
                var senhas = senhasET.ListarSenhas();
                return new OkObjectResult(senhas);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao listar as senhas: " + ex.Message);
            }
        }

        [HttpGet("/PesquisarSenhasPorCriterio")]
        [SwaggerOperation(Summary = "Pesquisar senhas por critério", Description = "Obtém senhas baseadas em um critério e valor de pesquisa.")]
        public static IActionResult PesquisarSenhasPorCriterio([FromQuery] string criterio, [FromQuery] string valorPesquisa)
        {
            try
            {
                var senhas = senhasET.PesquisarSenhasPorCriterio(criterio, valorPesquisa);
                return new OkObjectResult(senhas);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao pesquisar senhas por critério: " + ex.Message);
            }
        }

    }
}
