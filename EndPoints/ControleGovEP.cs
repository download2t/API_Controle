using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

public static class ControleGovEP
{
    private static ControleGovET controleGovET = new ControleGovET();

    // Adicionar ControleGov
    [SwaggerOperation(Summary = "Adicionar um novo controle governamental", Description = "Adiciona um novo controle governamental ao sistema.")]
    public static async Task<IResult> AdicionarControleGov(ControleGov controleGov)
    {
        try
        {
            string resultado = controleGovET.AdicionarControleGov(controleGov);
            return resultado == "OK" ? Results.Ok() : Results.BadRequest(resultado);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Atualizar ControleGov
    [SwaggerOperation(Summary = "Atualizar um controle governamental existente", Description = "Atualiza os dados de um controle governamental existente.")]
    public static async Task<IResult> AtualizarControleGov(ControleGov controleGov)
    {
        try
        {
            string resultado = controleGovET.AtualizarControleGov(controleGov);
            return resultado == "OK" ? Results.Ok() : Results.BadRequest(resultado);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Excluir ControleGov
    [SwaggerOperation(Summary = "Excluir um controle governamental", Description = "Remove um controle governamental do sistema pelo ID.")]
    public static async Task<IResult> ExcluirControleGov([FromRoute] int controleID)
    {
        try
        {
            bool resultado = controleGovET.ExcluirControleGov(controleID);
            return resultado ? Results.Ok() : Results.NotFound();
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Buscar ControleGov por ID
    [SwaggerOperation(Summary = "Buscar controle governamental por ID", Description = "Retorna um controle governamental pelo ID especificado.")]
    public static async Task<IResult> BuscarControleGovPorID([FromRoute] int controleID)
    {
        try
        {
            ControleGov controleGov = controleGovET.BuscarControleGovPorID(controleID);
            return controleGov != null ? Results.Json(controleGov) : Results.NotFound();
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Listar Todos os ControleGov
    [SwaggerOperation(Summary = "Listar todos os controles governamentais", Description = "Retorna uma lista de todos os controles governamentais cadastrados.")]
    public static async Task<IResult> ListarControleGov()
    {
        try
        {
            List<ControleGov> controles = controleGovET.ListarControleGov();
            return Results.Json(controles);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }

    // Listar ControleGov com Filtros
    [SwaggerOperation(Summary = "Listar controles governamentais com filtros", Description = "Retorna uma lista de controles governamentais filtrados por data e nome do funcionário.")]
    public static async Task<IResult> ListarControleGovComFiltros([FromQuery] DateTime? dataEntrada = null, [FromQuery] DateTime? dataSaida = null, [FromQuery] string nomeFuncionario = "")
    {
        try
        {
            List<ControleGov> controles = controleGovET.ListarControleGov(dataEntrada, dataSaida, nomeFuncionario);
            return Results.Json(controles);
        }
        catch
        {
            return Results.StatusCode(500);
        }
    }
}
