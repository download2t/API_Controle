using API_Loja.Entities;
using API_Loja.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace web_controle.EndPoints
{
    public static class CargosEP
    {
        // Adicionar Cargo
        [HttpPost("/Cargos")]
        [SwaggerOperation(Summary = "Adicionar um novo cargo", Description = "Adiciona um novo cargo ao sistema.")]
        public static IActionResult AdicionarCargo([FromBody] Cargo cargo)
        {
            try
            {
                CargosET cargosET = new CargosET();
                bool sucesso = cargosET.AdicionarCargo(cargo);

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

        // Listar Todos os Cargos
        [HttpGet("/Cargos")]
        [SwaggerOperation(Summary = "Listar todos os cargos", Description = "Retorna uma lista de todos os cargos cadastrados.")]
        public static IActionResult ListarTodosCargos()
        {
            try
            {
                CargosET cargosET = new CargosET();
                List<Cargo> cargos = cargosET.ListarCargos();

                return new JsonResult(cargos);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Buscar Cargo por ID
        [HttpGet("/Cargos/{id}")]
        [SwaggerOperation(Summary = "Buscar um cargo por ID", Description = "Retorna um cargo específico baseado no ID fornecido.")]
        public static IActionResult BuscarPorId([FromQuery] int id)
        {
            try
            {
                CargosET cargosET = new CargosET();
                Cargo cargo = cargosET.BuscarCargoPorId(id);

                if (cargo != null)
                {
                    return new JsonResult(cargo);
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

        // Buscar Cargos por Nome
        [HttpGet("/Cargos/Nome")]
        [SwaggerOperation(Summary = "Buscar cargos por nome", Description = "Retorna uma lista de cargos que correspondem ao nome fornecido.")]
        public static IActionResult BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                CargosET cargosET = new CargosET();
                List<Cargo> cargos = cargosET.BuscarCargosPorNome(nome);

                return new JsonResult(cargos);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Buscar Cargos por Pontos
        [HttpGet("/Cargos/Pontos")]
        [SwaggerOperation(Summary = "Buscar cargos por pontos", Description = "Retorna uma lista de cargos que correspondem aos pontos fornecidos.")]
        public static IActionResult BuscarPorPontos([FromQuery] string pontos)
        {
            try
            {
                CargosET cargosET = new CargosET();
                List<Cargo> cargos = cargosET.BuscarCargosPorPontos(pontos);

                return new JsonResult(cargos);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Atualizar Cargo
        [HttpPut("/Cargos")]
        [SwaggerOperation(Summary = "Atualizar um cargo", Description = "Atualiza os dados de um cargo existente.")]
        public static IActionResult AtualizarCargo([FromBody] Cargo cargo)
        {
            try
            {
                CargosET cargosET = new CargosET();
                bool sucesso = cargosET.AtualizarCargo(cargo);

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

        // Excluir Cargo por ID
        [HttpDelete("/Cargos/{id}")]
        [SwaggerOperation(Summary = "Excluir um cargo por ID", Description = "Exclui um cargo específico baseado no ID fornecido.")]
        public static IActionResult ExcluirCargoById([FromQuery] int id)
        {
            try
            {
                CargosET cargosET = new CargosET();
                bool sucesso = cargosET.ExcluirCargo(id);

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
