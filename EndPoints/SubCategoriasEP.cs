using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public class SubCategoriasEP
    {
        private static SubCategoriasET subCategoriasET = new SubCategoriasET();

        [HttpPost("/AdicionarSubcategoria")]
        [SwaggerOperation(Summary = "Adicionar subcategoria", Description = "Adiciona uma nova subcategoria.")]
        public static IActionResult AdicionarSubcategoria([FromBody] Subcategoria subcategoria)
        {
            try
            {
                subCategoriasET.AdicionarSubcategoria(subcategoria);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar subcategoria: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarSubcategoria")]
        [SwaggerOperation(Summary = "Atualizar subcategoria", Description = "Atualiza uma subcategoria existente.")]
        public static IActionResult AtualizarSubcategoria([FromBody] Subcategoria subcategoria)
        {
            try
            {
                subCategoriasET.AtualizarSubcategoria(subcategoria);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar subcategoria: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirSubcategoria/{subcategoriaId}")]
        [SwaggerOperation(Summary = "Excluir subcategoria", Description = "Exclui uma subcategoria pelo ID.")]
        public static IActionResult ExcluirSubcategoria([FromRoute] int subcategoriaId)
        {
            try
            {
                bool sucesso = subCategoriasET.ExcluirSubcategoria(subcategoriaId);
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
                return new BadRequestObjectResult("Erro ao excluir subcategoria: " + ex.Message);
            }
        }

        [HttpGet("/BuscarSubcategoriaPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar subcategoria por ID", Description = "Obtém uma subcategoria pelo ID.")]
        public static IActionResult BuscarSubcategoriaPorId([FromRoute] int id)
        {
            try
            {
                var subcategoria = subCategoriasET.BuscarSubcategoriaPorId(id);
                if (subcategoria != null)
                {
                    return new OkObjectResult(subcategoria);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao buscar subcategoria por ID: " + ex.Message);
            }
        }

        [HttpGet("/PesquisarSubcategoriasPorCriterio")]
        [SwaggerOperation(Summary = "Pesquisar subcategorias por critério", Description = "Obtém subcategorias baseadas em um critério de pesquisa.")]
        public static IActionResult PesquisarSubcategoriasPorCriterio([FromQuery] string criterio, [FromQuery] string valorPesquisa)
        {
            try
            {
                var subcategorias = subCategoriasET.PesquisarSubcategoriasPorCriterio(criterio, valorPesquisa);
                return new OkObjectResult(subcategorias);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao pesquisar subcategorias: " + ex.Message);
            }
        }

        [HttpGet("/ListarSubcategorias")]
        [SwaggerOperation(Summary = "Listar subcategorias", Description = "Obtém todas as subcategorias.")]
        public static IActionResult ListarSubcategorias()
        {
            try
            {
                var subcategorias = subCategoriasET.ListarSubcategorias();
                return new OkObjectResult(subcategorias);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao listar subcategorias: " + ex.Message);
            }
        }

        [HttpGet("/ListarSubcategoriasPorIDCategoria")]
        [SwaggerOperation(Summary = "Listar subcategorias por ID de categoria", Description = "Obtém subcategorias baseadas no ID da categoria.")]
        public static IActionResult ListarSubcategoriasPorIDCategoria([FromQuery] int? categoriaId)
        {
            try
            {
                var subcategorias = subCategoriasET.ListarSubcategoriasPorIDCategoria(categoriaId);
                return new OkObjectResult(subcategorias);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao listar subcategorias por ID de categoria: " + ex.Message);
            }
        }
    }
}
