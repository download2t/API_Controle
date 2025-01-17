using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public class SetoresEP
    {
        private static SetoresET setoresET = new SetoresET();

        [HttpPost("/AdicionarSetor")]
        [SwaggerOperation(Summary = "Adicionar setor", Description = "Adiciona um novo setor.")]
        public static IActionResult AdicionarSetor([FromBody] Setores setor)
        {
            try
            {
                setoresET.AdicionarSetor(setor);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar setor: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarSetor")]
        [SwaggerOperation(Summary = "Atualizar setor", Description = "Atualiza um setor existente.")]
        public static IActionResult AtualizarSetor([FromBody] Setores setor)
        {
            try
            {
                setoresET.AtualizarSetor(setor);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar setor: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirSetor/{setorId}")]
        [SwaggerOperation(Summary = "Excluir setor", Description = "Exclui um setor pelo ID.")]
        public static IActionResult ExcluirSetor([FromRoute] int setorId)
        {
            try
            {
                bool sucesso = setoresET.ExcluirSetor(setorId);
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
                return new BadRequestObjectResult("Erro ao excluir setor: " + ex.Message);
            }
        }

        [HttpGet("/BuscarSetorPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar setor por ID", Description = "Obtém um setor pelo ID.")]
        public static IActionResult BuscarSetorPorId([FromRoute] int id)
        {
            try
            {
                var setor = setoresET.BuscarSetorPorId(id);
                if (setor != null)
                {
                    return new OkObjectResult(setor);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao buscar setor por ID: " + ex.Message);
            }
        }

        [HttpGet("/BuscarSetorPorNome")]
        [SwaggerOperation(Summary = "Buscar setor por nome", Description = "Obtém setores baseados em um valor de pesquisa.")]
        public static IActionResult BuscarSetorPorNome([FromQuery] string valorPesquisa)
        {
            try
            {
                var setores = setoresET.BuscarSetorPorNome(valorPesquisa);
                return new OkObjectResult(setores);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao buscar setor por nome: " + ex.Message);
            }
        }

        [HttpGet("/ListarSetores")]
        [SwaggerOperation(Summary = "Listar setores", Description = "Obtém todos os setores.")]
        public static IActionResult ListarSetores()
        {
            try
            {
                var setores = setoresET.ListarSetores();
                return new OkObjectResult(setores);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao listar setores: " + ex.Message);
            }
        }
    }
}
