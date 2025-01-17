using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_CONTROLE.EndPoints
{
    public static class OrdemDeServicoEP
    {
        private static readonly OrdemDeServicoET ordemDeServicoET = new OrdemDeServicoET();

        // Adicionar Ordem de Serviço
        // POST /AdicionarOrdemDeServico
        [HttpPost("/AdicionarOrdemDeServico")]
        [SwaggerOperation(Summary = "Adicionar uma nova ordem de serviço", Description = "Adiciona uma nova ordem de serviço ao sistema.")]
        public static async Task<IResult> AdicionarOrdemDeServico([FromBody] OrdemDeServicoRequest request)
        {
            if (request == null || request.OrdemDeServico == null)
            {
                return Results.BadRequest("A ordem de serviço não pode ser nula.");
            }

            try
            {
                var resultado = await Task.Run(() => ordemDeServicoET.AdicionarOrdemDeServico(request.OrdemDeServico, request.FotosParaAdicionar));
                return resultado ? Results.Ok() : Results.BadRequest("Falha ao adicionar a ordem de serviço.");
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }
        // Atualizar Ordem de Serviço
        // PUT /AtualizarOrdemDeServico
        [HttpPut("/AtualizarOrdemDeServico")]
        [SwaggerOperation(Summary = "Atualizar uma ordem de serviço existente", Description = "Atualiza os dados de uma ordem de serviço existente.")]
        public static async Task<IResult> AtualizarOrdemDeServico([FromBody] OrdemDeServico ordemDeServico)
        {
            if (ordemDeServico == null)
            {
                return Results.BadRequest("A ordem de serviço não pode ser nula.");
            }

            try
            {
                var resultado = await Task.Run(() => ordemDeServicoET.AtualizarOrdemDeServico(ordemDeServico));
                return resultado ? Results.Ok() : Results.BadRequest("Falha ao atualizar a ordem de serviço.");
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }

        // Excluir Ordem de Serviço
        // DELETE /ExcluirOrdemDeServico/{ordemDeServicoId}
        [HttpDelete("/ExcluirOrdemDeServico/{ordemDeServicoId}")]
        [SwaggerOperation(Summary = "Excluir uma ordem de serviço", Description = "Remove uma ordem de serviço do sistema pelo ID.")]
        public static async Task<IResult> ExcluirOrdemDeServico([FromRoute] int ordemDeServicoId)
        {
            if (ordemDeServicoId <= 0)
            {
                return Results.BadRequest("ID da ordem de serviço deve ser maior que zero.");
            }

            try
            {
                var resultado = await Task.Run(() => ordemDeServicoET.ExcluirOrdemDeServico(ordemDeServicoId));
                return resultado ? Results.Ok() : Results.NotFound();
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }

        // Buscar Ordem de Serviço por ID
        // GET /BuscarOrdemDeServicoPorId/{id}
        [HttpGet("/BuscarOrdemDeServicoPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar ordem de serviço por ID", Description = "Retorna uma ordem de serviço pelo ID especificado.")]
        public static async Task<IResult> BuscarOrdemDeServicoPorId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return Results.BadRequest("ID da ordem de serviço deve ser maior que zero.");
            }

            try
            {
                var ordemDeServico = await Task.Run(() => ordemDeServicoET.BuscarOrdemDeServicoPorId(id));
                return ordemDeServico != null ? Results.Json(ordemDeServico) : Results.NotFound();
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }

        // Listar Todas as Ordens de Serviço
        // GET /ListarOrdensDeServico
        [HttpGet("/ListarOrdensDeServico")]
        [SwaggerOperation(Summary = "Listar todas as ordens de serviço", Description = "Retorna uma lista de todas as ordens de serviço cadastradas.")]
        public static async Task<IResult> ListarOrdensDeServico()
        {
            try
            {
                var ordensDeServico = await Task.Run(() => ordemDeServicoET.ListarOrdensDeServico());
                return Results.Json(ordensDeServico);
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }

        // Pesquisar Ordens de Serviço por Critério
        // GET /PesquisarOrdensDeServicoPorCriterio
        [HttpGet("/PesquisarOrdensDeServicoPorCriterio")]
        [SwaggerOperation(Summary = "Pesquisar ordens de serviço por critério", Description = "Retorna uma lista de ordens de serviço que correspondem ao critério e valor de pesquisa especificados.")]
        public static async Task<IResult> PesquisarOrdensDeServicoPorCriterio([FromQuery] string criterio, [FromQuery] string valorPesquisa)
        {
            if (string.IsNullOrEmpty(criterio) || string.IsNullOrEmpty(valorPesquisa))
            {
                return Results.BadRequest("Critério e valor de pesquisa não podem ser nulos ou vazios.");
            }

            try
            {
                var ordensDeServico = await Task.Run(() => ordemDeServicoET.PesquisarOsPorCriterio(criterio, valorPesquisa));
                return Results.Json(ordensDeServico);
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }
    }
}
public class OrdemDeServicoRequest
{
    public OrdemDeServico OrdemDeServico { get; set; }
    public List<Fotos> FotosParaAdicionar { get; set; } = new List<Fotos>();
}

public class OrdemDeServicoUpdateRequest
{
    public OrdemDeServico OrdemDeServico { get; set; }
    public List<Fotos> FotosParaAdicionar { get; set; } = new List<Fotos>();
    public List<int> FotosParaExcluir { get; set; } = new List<int>();
}
