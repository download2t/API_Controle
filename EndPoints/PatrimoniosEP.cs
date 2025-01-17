using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class PatrimoniosEP
    {
        private static PatrimoniosET patrimoniosET = new PatrimoniosET();

        [HttpPost("/AdicionarPatrimonio")]
        [SwaggerOperation(Summary = "Adicionar patrimônio", Description = "Adiciona um novo patrimônio.")]
        public static IActionResult AdicionarPatrimonio([FromBody] Patrimonios patrimonio)
        {
            try
            {
                patrimoniosET.AdicionarPatrimonio(patrimonio);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar patrimônio: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarPatrimonio")]
        [SwaggerOperation(Summary = "Atualizar patrimônio", Description = "Atualiza um patrimônio existente.")]
        public static IActionResult AtualizarPatrimonio([FromBody] Patrimonios patrimonio)
        {
            try
            {
                patrimoniosET.AtualizarPatrimonio(patrimonio);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar patrimônio: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarPatrimonioSemFoto")]
        [SwaggerOperation(Summary = "Atualizar patrimônio sem foto", Description = "Atualiza um patrimônio existente sem alterar a foto.")]
        public static IActionResult AtualizarPatrimonioSemFoto([FromBody] Patrimonios patrimonio)
        {
            try
            {
                patrimoniosET.AtualizarPatrimonioSemFoto(patrimonio);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar patrimônio sem foto: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirPatrimonio/{id}")]
        [SwaggerOperation(Summary = "Excluir patrimônio", Description = "Exclui um patrimônio pelo ID.")]
        public static IActionResult ExcluirPatrimonio([FromRoute] int id)
        {
            bool sucesso = patrimoniosET.ExcluirPatrimonio(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("/BuscarPatrimonioPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar patrimônio por ID", Description = "Busca um patrimônio pelo ID.")]
        public static IActionResult BuscarPatrimonioPorId([FromRoute] int id)
        {
            var patrimonio = patrimoniosET.BuscarPatrimonioPorId(id);
            if (patrimonio != null)
            {
                return new OkObjectResult(patrimonio);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("/ListarPatrimonios")]
        [SwaggerOperation(Summary = "Listar patrimônios", Description = "Lista todos os patrimônios.")]
        public static IActionResult ListarPatrimonios()
        {
            var patrimonios = patrimoniosET.ListarPatrimonios();
            return new OkObjectResult(patrimonios);
        }

        [HttpGet("/PesquisarPatrimoniosPorCriterio/{criterio}/{valorPesquisa}")]
        [SwaggerOperation(Summary = "Pesquisar patrimônios por critério", Description = "Pesquisa patrimônios por critério e valor de pesquisa.")]
        public static IActionResult PesquisarPatrimoniosPorCriterio([FromRoute] string criterio, [FromRoute] string valorPesquisa)
        {
            var patrimonios = patrimoniosET.PesquisarPatrimoniosPorCriterio(criterio, valorPesquisa);
            return new OkObjectResult(patrimonios);
        }

        [HttpGet("/RelatorioPatrimonios")]
        [SwaggerOperation(Summary = "Relatório de patrimônios", Description = "Gera um relatório de patrimônios.")]
        public static IActionResult RelatorioPatrimonios()
        {
            var relatorio = patrimoniosET.Relatorios();
            return new OkObjectResult(relatorio);
        }

        [HttpGet("/RelatorioPatrimonios2")]
        [SwaggerOperation(Summary = "Relatório de patrimônios 2", Description = "Gera um segundo relatório de patrimônios.")]
        public static IActionResult RelatorioPatrimonios2()
        {
            var relatorio = patrimoniosET.Relatorios2();
            return new OkObjectResult(relatorio);
        }
    }
}
