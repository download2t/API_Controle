using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class OpcoesEP
    {
        private static OpcoesET opcoesET = new OpcoesET();

        // Adicionar Opções
        // POST /AdicionarOpcoes
        [HttpPost("/AdicionarOpcoes")]
        [SwaggerOperation(Summary = "Adicionar opções", Description = "Adiciona uma lista de opções de menu.")]
        public static IActionResult AdicionarOpcoes([FromBody] HashSet<Opcoes> opcoes)
        {
            bool sucesso = opcoesET.SalvarMenu(opcoes);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        // Obter Opções do Menu
        // GET /ObterOpcoes
        [HttpGet("/ObterOpcoes")]
        [SwaggerOperation(Summary = "Obter opções do menu", Description = "Obtém todas as opções de menu do banco de dados.")]
        public static IActionResult ObterOpcoes()
        {
            var opcoesMenu = opcoesET.ObterOpcoesDoMenuDoBanco();
            return new OkObjectResult(opcoesMenu);
        }
    }
}
