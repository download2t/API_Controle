using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class ManutencaoEP
    {
        private static ManutencaoET manutencaoET = new ManutencaoET();

        [HttpPost("/AdicionarManutencao")]
        [SwaggerOperation(Summary = "Adicionar manutenção", Description = "Adiciona uma nova manutenção.")]
        public static IActionResult AdicionarManutencao([FromBody] Manutencao manutencao)
        {
            try
            {
                manutencaoET.AdicionarManutencao(manutencao);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar manutenção: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarManutencao")]
        [SwaggerOperation(Summary = "Atualizar manutenção", Description = "Atualiza uma manutenção existente.")]
        public static IActionResult AtualizarManutencao([FromBody] Manutencao manutencao)
        {
            try
            {
                manutencaoET.AtualizarManutencao(manutencao);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar manutenção: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirManutencao/{id}")]
        [SwaggerOperation(Summary = "Excluir manutenção", Description = "Exclui uma manutenção pelo ID.")]
        public static IActionResult ExcluirManutencao([FromRoute] int id)
        {
            bool sucesso = manutencaoET.ExcluirManutencao(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("/BuscarManutencaoPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar manutenção por ID", Description = "Busca uma manutenção pelo ID.")]
        public static IActionResult BuscarManutencaoPorId([FromRoute] int id)
        {
            var manutencao = manutencaoET.BuscarManutencaoPorId(id);
            if (manutencao != null)
            {
                return new OkObjectResult(manutencao);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("/ListarManutencoes")]
        [SwaggerOperation(Summary = "Listar manutenções", Description = "Lista todas as manutenções.")]
        public static IActionResult ListarManutencoes()
        {
            var manutencoes = manutencaoET.ListarManutencoes();
            return new OkObjectResult(manutencoes);
        }

        [HttpGet("/PesquisarManutencoesPorCriterio/{criterio}/{valorPesquisa}")]
        [SwaggerOperation(Summary = "Pesquisar manutenções por critério", Description = "Pesquisa manutenções por critério e valor de pesquisa.")]
        public static IActionResult PesquisarManutencoesPorCriterio([FromRoute] string criterio, [FromRoute] string valorPesquisa)
        {
            var manutencoes = manutencaoET.PesquisarManutencoesPorCriterio(criterio, valorPesquisa);
            return new OkObjectResult(manutencoes);
        }

        [HttpGet("/RelatorioManutencao")]
        [SwaggerOperation(Summary = "Relatório de manutenção", Description = "Gera um relatório de manutenção.")]
        public static IActionResult RelatorioManutencao()
        {
            var relatorio = manutencaoET.RelatorioManutencao();
            return new OkObjectResult(relatorio);
        }

        [HttpGet("/RelatorioManutencao2")]
        [SwaggerOperation(Summary = "Relatório de manutenção 2", Description = "Gera um segundo relatório de manutenção.")]
        public static IActionResult RelatorioManutencao2()
        {
            var relatorio = manutencaoET.RelatorioManutencao2();
            return new OkObjectResult(relatorio);
        }
    }
}
