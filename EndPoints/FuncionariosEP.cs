using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class FuncionariosEP
    {
        private static FuncionariosET funcionariosET = new FuncionariosET();

        // Adicionar Funcionário
        // POST /AdicionarFuncionario
        [HttpPost("/AdicionarFuncionario")]
        [SwaggerOperation(Summary = "Adicionar funcionário", Description = "Adiciona um novo funcionário.")]
        public static IActionResult AdicionarFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                funcionariosET.AdicionarFuncionario(funcionario);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar funcionário: " + ex.Message);
            }
        }

        // Atualizar Funcionário
        // PUT /AtualizarFuncionario
        [HttpPut("/AtualizarFuncionario")]
        [SwaggerOperation(Summary = "Atualizar funcionário", Description = "Atualiza um funcionário existente.")]
        public static IActionResult AtualizarFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                funcionariosET.AtualizarFuncionario(funcionario);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        // Excluir Funcionário
        // DELETE /ExcluirFuncionario/{id}
        [HttpDelete("/ExcluirFuncionario/{id}")]
        [SwaggerOperation(Summary = "Excluir funcionário", Description = "Exclui um funcionário pelo ID.")]
        public static IActionResult ExcluirFuncionario([FromRoute] int id)
        {
            bool sucesso = funcionariosET.ExcluirFuncionario(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        // Buscar Funcionário por ID
        // GET /BuscarFuncionarioPorId/{id}
        [HttpGet("/BuscarFuncionarioPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar funcionário por ID", Description = "Busca um funcionário pelo ID.")]
        public static IActionResult BuscarFuncionarioPorId([FromRoute] int id)
        {
            var funcionario = funcionariosET.BuscarFuncionarioPorId(id);
            if (funcionario != null)
            {
                return new OkObjectResult(funcionario);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // Listar Funcionários
        // GET /ListarFuncionarios/{usuarioAtivo}
        [HttpGet("/ListarFuncionarios/{usuarioAtivo}")]
        [SwaggerOperation(Summary = "Listar funcionários", Description = "Lista todos os funcionários ativos ou inativos.")]
        public static IActionResult ListarFuncionarios([FromRoute] string usuarioAtivo)
        {
            var funcionarios = funcionariosET.ListarFuncionarios(usuarioAtivo);
            return new OkObjectResult(funcionarios);
        }

        // Pesquisar Funcionários por Critério
        // GET /PesquisarFuncionariosPorCriterio/{criterio}/{valorPesquisa}
        [HttpGet("/PesquisarFuncionariosPorCriterio/{criterio}/{valorPesquisa}")]
        [SwaggerOperation(Summary = "Pesquisar funcionários por critério", Description = "Pesquisa funcionários por critério e valor de pesquisa.")]
        public static IActionResult PesquisarFuncionariosPorCriterio([FromRoute] string criterio, [FromRoute] string valorPesquisa)
        {
            var funcionarios = funcionariosET.PesquisarFuncionariosPorCriterio(criterio, valorPesquisa);
            return new OkObjectResult(funcionarios);
        }
    }
}
