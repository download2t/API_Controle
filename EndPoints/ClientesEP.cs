using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class ClientesEP
    {
        // Adicionar Cliente
        [HttpPost("/Clientes")]
        [SwaggerOperation(Summary = "Adicionar um novo cliente", Description = "Adiciona um novo cliente ao sistema.")]
        public static IActionResult AdicionarCliente([FromBody] Clientes cliente)
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                clientesET.AdicionarCliente(cliente);

                return new OkResult();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Listar Todos os Clientes
        [HttpGet("/Clientes")]
        [SwaggerOperation(Summary = "Listar todos os clientes", Description = "Retorna uma lista de todos os clientes cadastrados.")]
        public static IActionResult ListarTodosClientes()
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                List<Clientes> clientes = clientesET.ListarClientes();

                return new JsonResult(clientes);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Buscar Cliente por ID
        [HttpGet("/Clientes/{id}")]
        [SwaggerOperation(Summary = "Buscar um cliente por ID", Description = "Retorna um cliente específico baseado no ID fornecido.")]
        public static IActionResult BuscarPorId([FromRoute] int id)
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                Clientes cliente = clientesET.BuscarClientePorId(id);

                if (cliente != null)
                {
                    return new JsonResult(cliente);
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

        // Buscar Clientes por Nome
        [HttpGet("/Clientes/Nome")]
        [SwaggerOperation(Summary = "Buscar clientes por nome", Description = "Retorna uma lista de clientes que correspondem ao nome fornecido.")]
        public static IActionResult BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                List<Clientes> clientes = clientesET.PesquisarClientesPorCriterio("Nome", nome);

                return new JsonResult(clientes);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Atualizar Cliente
        [HttpPut("/Clientes")]
        [SwaggerOperation(Summary = "Atualizar um cliente", Description = "Atualiza os dados de um cliente existente.")]
        public static IActionResult AtualizarCliente([FromBody] Clientes cliente)
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                clientesET.AtualizarCliente(cliente);

                return new OkResult();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        // Excluir Cliente por ID
        [HttpDelete("/Clientes/{id}")]
        [SwaggerOperation(Summary = "Excluir um cliente por ID", Description = "Exclui um cliente específico baseado no ID fornecido.")]
        public static IActionResult ExcluirClienteById([FromRoute] int id)
        {
            try
            {
                ClientesET clientesET = new ClientesET();
                bool sucesso = clientesET.ExcluirCliente(id);

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

        // Listar Todos os Clientes
        [HttpGet("/Clientes")]
        [SwaggerOperation(Summary = "Listar clientes por critério", Description = "Retorna uma lista de todos os clientes que atendem estes critérios.")]
        public static IResult PesquisarClientesPorCriterio(HttpContext httpContext, string criterio, string valorPesquisa)
        {
           
                ClientesET clientesET = new ClientesET();
                List<Clientes> clientes = clientesET.PesquisarClientesPorCriterio(criterio,valorPesquisa);

               return Results.Ok(clientes);
            
        }

    }
}
