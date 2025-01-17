using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class MensagensEP
    {
        private static MensagensET mensagensET = new MensagensET();

        // Adicionar Mensagem
        // POST /AdicionarMensagem
        [HttpPost("/AdicionarMensagem")]
        [SwaggerOperation(Summary = "Adicionar mensagem", Description = "Adiciona uma nova mensagem.")]
        public static IActionResult AdicionarMensagem([FromBody] Mensagens mensagem)
        {
            var result = mensagensET.AdicionarMensagem(mensagem);
            if (result == "OK")
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(result);
            }
        }

        // Adicionar Mensagens em Lote
        // POST /AdicionarMensagensEmLote
        [HttpPost("/AdicionarMensagensEmLote")]
        [SwaggerOperation(Summary = "Adicionar mensagens em lote", Description = "Adiciona uma lista de mensagens.")]
        public static IActionResult AdicionarMensagensEmLote([FromBody] List<Mensagens> mensagens)
        {
            var erros = mensagensET.AdicionarEmLote(mensagens);
            if (erros.Count == 0)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(erros);
            }
        }

        // Atualizar Mensagem
        // PUT /AtualizarMensagem
        [HttpPut("/AtualizarMensagem")]
        [SwaggerOperation(Summary = "Atualizar mensagem", Description = "Atualiza uma mensagem existente.")]
        public static IActionResult AtualizarMensagem([FromBody] Mensagens mensagem)
        {
            var result = mensagensET.AtualizarMensagem(mensagem);
            if (result == "OK")
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(result);
            }
        }

        // Excluir Mensagem
        // DELETE /ExcluirMensagem/{id}
        [HttpDelete("/ExcluirMensagem/{id}")]
        [SwaggerOperation(Summary = "Excluir mensagem", Description = "Exclui uma mensagem pelo ID.")]
        public static IActionResult ExcluirMensagem([FromRoute] int id)
        {
            bool sucesso = mensagensET.ExcluirMensagem(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        // Excluir Todas as Mensagens por Contato ID
        // DELETE /ExcluirTodasMensagensPorContatoId/{contatoId}
        [HttpDelete("/ExcluirTodasMensagensPorContatoId/{contatoId}")]
        [SwaggerOperation(Summary = "Excluir todas as mensagens por Contato ID", Description = "Exclui todas as mensagens de um contato específico pelo ID.")]
        public static IActionResult ExcluirTodasMensagensPorContatoId([FromRoute] int contatoId)
        {
            bool sucesso = mensagensET.ExcluirTodasPorId(contatoId);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        // Buscar Mensagem por ID
        // GET /BuscarMensagemPorId/{id}
        [HttpGet("/BuscarMensagemPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar mensagem por ID", Description = "Busca uma mensagem pelo ID.")]
        public static IActionResult BuscarMensagemPorId([FromRoute] int id)
        {
            var mensagem = mensagensET.BuscarMensagemPorId(id);
            if (mensagem != null)
            {
                return new OkObjectResult(mensagem);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        // Listar Todas as Mensagens
        // GET /ListarMensagens
        [HttpGet("/ListarMensagens")]
        [SwaggerOperation(Summary = "Listar todas as mensagens", Description = "Lista todas as mensagens.")]
        public static IActionResult ListarMensagens()
        {
            var mensagens = mensagensET.ListarMensagens2();
            return new OkObjectResult(mensagens);
        }

        // Listar Mensagens com Filtros
        // GET /ListarMensagensFiltradas
        [HttpGet("/ListarMensagensFiltradas")]
        [SwaggerOperation(Summary = "Listar mensagens com filtros", Description = "Lista mensagens com base em critérios de filtro.")]
        public static IActionResult ListarMensagensFiltradas(
            [FromQuery] string criterioLista,
            [FromQuery] string criterioPesquisa,
            [FromQuery] string valorPesquisa,
            [FromQuery] DateTime? dataInicio = null,
            [FromQuery] DateTime? dataFim = null)
        {
            var mensagens = mensagensET.ListarMensagens(criterioLista, criterioPesquisa, valorPesquisa, dataInicio, dataFim);
            return new OkObjectResult(mensagens);
        }


    }
}
