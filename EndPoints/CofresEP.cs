using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class CofresEP
    {
        // Adicionar Cofre
        [HttpPost("/Cofres")]
        [SwaggerOperation(Summary = "Adicionar um novo cofre", Description = "Adiciona um novo cofre ao sistema.")]
        public static IActionResult AdicionarCofre([FromBody] Cofres cofre)
        {
            try
            {
                CofresET cofresET = new CofresET();
                cofresET.AdicionarCofre(cofre);
                return new OkResult();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Atualizar Cofre
        [HttpPut("/Cofres")]
        [SwaggerOperation(Summary = "Atualizar um cofre existente", Description = "Atualiza os dados de um cofre existente.")]
        public static IActionResult AtualizarCofre([FromBody] Cofres cofre)
        {
            try
            {
                CofresET cofresET = new CofresET();
                cofresET.AtualizarCofre(cofre);
                return new OkResult();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Excluir Cofre por ID
        [HttpDelete("/Cofres/{id}")]
        [SwaggerOperation(Summary = "Excluir um cofre por ID", Description = "Remove um cofre do sistema pelo ID.")]
        public static IActionResult ExcluirCofre([FromRoute] int id)
        {
            try
            {
                CofresET cofresET = new CofresET();
                bool sucesso = cofresET.ExcluirCofre(id);

                if (sucesso)
                {
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Buscar Cofre por ID
        [HttpGet("/Cofres/{id}")]
        [SwaggerOperation(Summary = "Buscar um cofre por ID", Description = "Retorna um cofre pelo ID especificado.")]
        public static IActionResult BuscarCofrePorId([FromRoute] int id)
        {
            try
            {
                CofresET cofresET = new CofresET();
                Cofres cofre = cofresET.BuscarCofrePorId(id);

                if (cofre != null)
                {
                    return new JsonResult(cofre);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Listar Todos os Cofres
        [HttpGet("/Cofres")]
        [SwaggerOperation(Summary = "Listar todos os cofres", Description = "Retorna uma lista de todos os cofres cadastrados.")]
        public static IActionResult ListarCofres()
        {
            try
            {
                CofresET cofresET = new CofresET();
                List<Cofres> cofres = cofresET.ListarCofres();
                return new JsonResult(cofres);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Pesquisar Cofres
        [HttpGet("/Cofres/Pesquisar")]
        [SwaggerOperation(Summary = "Pesquisar cofres", Description = "Retorna uma lista de cofres que correspondem ao valor de pesquisa.")]
        public static IActionResult PesquisarCofres([FromQuery] string valor)
        {
            try
            {
                CofresET cofresET = new CofresET();
                List<Cofres> cofres = cofresET.PesquisarCofres(valor);
                return new JsonResult(cofres);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
