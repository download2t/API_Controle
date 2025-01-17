using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class FotosEP
    {
        private static FotosET fotosET = new FotosET();

        // Adicionar Fotos
        // POST /AdicionarFotos
        [HttpPost("/AdicionarFotos")]
        [SwaggerOperation(Summary = "Adicionar fotos", Description = "Adiciona uma lista de fotos.")]
        public static IActionResult AdicionarFotos([FromBody] List<Fotos> fotos)
        {
            var erros = fotosET.AdicionarFotos(fotos);
            if (erros.Count == 0)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(erros);
            }
        }

        // Excluir Foto
        // DELETE /ExcluirFoto/{id}
        [HttpDelete("/ExcluirFoto/{id}")]
        [SwaggerOperation(Summary = "Excluir foto", Description = "Exclui uma foto pelo ID.")]
        public static IActionResult ExcluirFoto([FromRoute] int id)
        {
            bool sucesso = fotosET.ExcluirFoto(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        // Buscar Foto por ID
        // GET /BuscarFotoPorId/{id}
        [HttpGet("/BuscarFotoPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar foto por ID", Description = "Busca uma foto pelo ID.")]
        public static IActionResult BuscarFotoPorId([FromRoute] int id)
        {
            var foto = fotosET.BuscarFotoPorId(id);
            if (foto != null)
            {
                return new OkObjectResult(foto);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // Listar Fotos
        // GET /ListarFotos
        [HttpGet("/ListarFotos")]
        [SwaggerOperation(Summary = "Listar todas as fotos", Description = "Lista todas as fotos.")]
        public static IActionResult ListarFotos()
        {
            var fotos = fotosET.ListarFotos();
            return new OkObjectResult(fotos);
        }

        // Listar Fotos por Ordem de Serviço
        // GET /ListarFotosDaOrdemDeServico/{ordemDeServicoId}
        [HttpGet("/ListarFotosDaOrdemDeServico/{ordemDeServicoId}")]
        [SwaggerOperation(Summary = "Listar fotos da ordem de serviço", Description = "Lista todas as fotos de uma ordem de serviço específica.")]
        public static IActionResult ListarFotosDaOrdemDeServico([FromRoute] int ordemDeServicoId)
        {
            var fotos = fotosET.ListarFotosDaOrdemDeServico(ordemDeServicoId);
            return new OkObjectResult(fotos);
        }
    }
}
