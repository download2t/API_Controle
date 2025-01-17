using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class PermissaoMenuEP
    {
        private static PermissaoMenuET permissaoMenuET = new PermissaoMenuET();

        [HttpPost("/AdicionarPermissaoMenu")]
        [SwaggerOperation(Summary = "Adicionar permissão de menu", Description = "Adiciona uma nova permissão de menu.")]
        public static IActionResult AdicionarPermissaoMenu([FromBody] PermissaoMenu permissaoMenu)
        {
            try
            {
                permissaoMenuET.AdicionarPermissaoMenu(permissaoMenu);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar permissão de menu: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarPermissaoMenu")]
        [SwaggerOperation(Summary = "Atualizar permissão de menu", Description = "Atualiza uma permissão de menu existente.")]
        public static IActionResult AtualizarPermissaoMenu([FromBody] PermissaoMenu permissaoMenu)
        {
            try
            {
                permissaoMenuET.AtualizarPermissaoMenu(permissaoMenu);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar permissão de menu: " + ex.Message);
            }
        }

        [HttpDelete("/RemoverPermissaoMenu/{usuarioId}/{menuOpcaoId}")]
        [SwaggerOperation(Summary = "Remover permissão de menu", Description = "Remove uma permissão de menu pelo ID do usuário e ID da opção do menu.")]
        public static IActionResult RemoverPermissaoMenu([FromRoute] int usuarioId, [FromRoute] int menuOpcaoId)
        {
            try
            {
                permissaoMenuET.RemoverPermissaoMenu(usuarioId, menuOpcaoId);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao remover permissão de menu: " + ex.Message);
            }
        }

        [HttpGet("/ObterPermissaoMenu/{usuarioId}/{menuOpcaoId}")]
        [SwaggerOperation(Summary = "Obter permissão de menu", Description = "Obtém a permissão de menu pelo ID do usuário e ID da opção do menu.")]
        public static IActionResult ObterPermissaoMenu([FromRoute] int usuarioId, [FromRoute] int menuOpcaoId)
        {
            try
            {
                var permissaoMenu = permissaoMenuET.ObterPermissaoMenu(usuarioId, menuOpcaoId);
                if (permissaoMenu != null)
                {
                    return new OkObjectResult(permissaoMenu);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao obter permissão de menu: " + ex.Message);
            }
        }

        [HttpGet("/ObterPermissoesPorUsuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Obter permissões por usuário", Description = "Obtém todas as permissões de menu para um usuário específico.")]
        public static IActionResult ObterPermissoesPorUsuario([FromRoute] int usuarioId)
        {
            try
            {
                var permissoes = permissaoMenuET.ObterPermissoesPorUsuario(usuarioId);
                return new OkObjectResult(permissoes);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao obter permissões do usuário: " + ex.Message);
            }
        }

        [HttpGet("/ObterOpcoesMenuExceto/{tipo}")]
        [SwaggerOperation(Summary = "Obter opções de menu exceto", Description = "Obtém as opções de menu exceto para um tipo específico.")]
        public static IActionResult ObterOpcoesMenuExceto([FromRoute] string tipo)
        {
            try
            {
                var opcoes = permissaoMenuET.ObterOpcoesMenuExceto(tipo);
                return new OkObjectResult(opcoes);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao obter opções de menu: " + ex.Message);
            }
        }
    }
}
