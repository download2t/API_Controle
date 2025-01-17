using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

public static class ContatosEP
{
    private static readonly ContatosET contatosET = new ContatosET();

    // Adicionar Contato
    // POST /AdicionarContato
    [HttpPost("/AdicionarContato")]
    [SwaggerOperation(Summary = "Adicionar um novo contato", Description = "Adiciona um novo contato ao sistema.")]
    public static async Task<IResult> AdicionarContato([FromBody] Contatos contato)
    {
        if (contato == null)
        {
            return Results.BadRequest("O contato não pode ser nulo.");
        }

        try
        {
            var resultado = await Task.Run(() => contatosET.AdicionarContato(contato));
            return resultado ? Results.Ok() : Results.BadRequest("Falha ao adicionar o contato.");
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Atualizar Contato
    // PUT /AtualizarContato
    [HttpPut("/AtualizarContato")]
    [SwaggerOperation(Summary = "Atualizar um contato existente", Description = "Atualiza os dados de um contato existente.")]
    public static async Task<IResult> AtualizarContato([FromBody] Contatos contato)
    {
        if (contato == null)
        {
            return Results.BadRequest("O contato não pode ser nulo.");
        }

        try
        {
            var resultado = await Task.Run(() => contatosET.AtualizarContato(contato));
            return resultado ? Results.Ok() : Results.BadRequest("Falha ao atualizar o contato.");
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Excluir Contato
    // DELETE /ExcluirContato/{contatoId}
    [HttpDelete("/ExcluirContato/{contatoId}")]
    [SwaggerOperation(Summary = "Excluir um contato", Description = "Remove um contato do sistema pelo ID.")]
    public static async Task<IResult> ExcluirContato([FromRoute] int contatoId)
    {
        if (contatoId <= 0)
        {
            return Results.BadRequest("ID do contato deve ser maior que zero.");
        }

        try
        {
            var resultado = await Task.Run(() => contatosET.ExcluirContato(contatoId));
            return resultado ? Results.Ok() : Results.NotFound();
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Buscar Contato por ID
    // GET /BuscarContatoPorId/{id}
    [HttpGet("/BuscarContatoPorId/{id}")]
    [SwaggerOperation(Summary = "Buscar contato por ID", Description = "Retorna um contato pelo ID especificado.")]
    public static async Task<IResult> BuscarContatoPorId([FromRoute] int id)
    {
        if (id <= 0)
        {
            return Results.BadRequest("ID do contato deve ser maior que zero.");
        }

        try
        {
            var contato = await Task.Run(() => contatosET.BuscarContatoPorId(id));
            return contato != null ? Results.Json(contato) : Results.NotFound();
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Listar Todos os Contatos
    // GET /ListarContatos
    [HttpGet("/ListarContatos")]
    [SwaggerOperation(Summary = "Listar todos os contatos", Description = "Retorna uma lista de todos os contatos cadastrados.")]
    public static async Task<IResult> ListarContatos()
    {
        try
        {
            var contatos = await Task.Run(() => contatosET.ListarContatos());
            return Results.Json(contatos);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Pesquisar Contatos por Critério
    // GET /PesquisarContatosPorCriterio
    [HttpGet("/PesquisarContatosPorCriterio")]
    [SwaggerOperation(Summary = "Pesquisar contatos por critério", Description = "Retorna uma lista de contatos que correspondem ao critério e valor de pesquisa especificados.")]
    public static async Task<IResult> PesquisarContatosPorCriterio([FromQuery] string criterio, [FromQuery] string valorPesquisa)
    {
        if (string.IsNullOrEmpty(criterio) || string.IsNullOrEmpty(valorPesquisa))
        {
            return Results.BadRequest("Critério e valor de pesquisa não podem ser nulos ou vazios.");
        }

        try
        {
            var contatos = await Task.Run(() => contatosET.PesquisarContatosPorCriterio(criterio, valorPesquisa));
            return Results.Json(contatos);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }
}
