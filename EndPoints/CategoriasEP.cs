using API_Loja.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace web_controle.EndPoints
{
    public static class CategoriasEP
    {
        // Adicionar Categoria
        [HttpPost("/Categorias")]
        [SwaggerOperation(Summary = "Adicionar uma nova categoria", Description = "Adiciona uma nova categoria ao sistema.")]
        public static IActionResult AdicionarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                bool sucesso = CategoriaET.AdicionarCategoria(categoria);

                if (sucesso)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Listar Todas as Categorias
        [HttpGet("/Categorias")]
        [SwaggerOperation(Summary = "Listar todas as categorias", Description = "Retorna uma lista de todas as categorias cadastradas.")]
        public static IActionResult ListarTodasCategorias()
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                List<Categoria> categorias = CategoriaET.ListarCategorias();

                return new JsonResult(categorias);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Buscar Categoria por ID
        [HttpGet("/Categorias/{id}")]
        [SwaggerOperation(Summary = "Buscar categoria por ID", Description = "Retorna uma categoria específica com base no ID.")]
        public static IActionResult BuscarPorId([FromRoute] int id)
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                Categoria categoria = CategoriaET.BuscarCategoriaPorId(id);

                if (categoria != null)
                {
                    return new JsonResult(categoria);
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

        // Buscar Categorias por Nome
        [HttpGet("/Categorias/BuscarPorNome")]
        [SwaggerOperation(Summary = "Buscar categorias por nome", Description = "Retorna uma lista de categorias que correspondem ao nome fornecido.")]
        public static IActionResult BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                List<Categoria> categorias = CategoriaET.BuscarCategoriaPorNome(nome);

                return new JsonResult(categorias);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Atualizar Categoria
        [HttpPut("/Categorias")]
        [SwaggerOperation(Summary = "Atualizar uma categoria existente", Description = "Atualiza os detalhes de uma categoria existente.")]
        public static IActionResult AtualizarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                bool sucesso = CategoriaET.AtualizarCategoria(categoria);

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

        // DELETE Categoria
        [HttpDelete("/Categorias/{id}")]
        [SwaggerOperation(Summary = "Excluir uma categoria", Description = "Remove uma categoria do sistema com base no ID.")]
        public static IActionResult ExcluirCategoriaById([FromRoute] int id)
        {
            try
            {
                CategoriaET CategoriaET = new CategoriaET();
                bool sucesso = CategoriaET.ExcluirCategoria(id);

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
    }
}
