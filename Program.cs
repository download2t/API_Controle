using API_CONTROLE.EndPoints;
using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Builder;
using web_controle.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeamento dos endpoints de MensagensEP
app.MapGet("/ListarMensagensFiltradas", (string criterioLista, string criterioPesquisa, string valorPesquisa) =>
{
    var mensagens = MensagensEP.ListarMensagensFiltradas(criterioLista, criterioPesquisa, valorPesquisa, null, null);
    return Results.Ok(mensagens);
});
app.MapPost("/AdicionarMensagem", MensagensEP.AdicionarMensagem);
app.MapPut("/AtualizarMensagem", MensagensEP.AtualizarMensagem);
app.MapDelete("/ExcluirMensagem/{id}", MensagensEP.ExcluirMensagem);
app.MapGet("/BuscarMensagemPorId/{id}", MensagensEP.BuscarMensagemPorId);
app.MapGet("/ListarMensagens", MensagensEP.ListarMensagens);

// Mapeamento dos Endpoints para ApiWhatsApp
app.MapPost("/EnviarMensagem", ApiWhatsAppEP.EnviarMensagem).WithName("EnviarMensagem");

// Mapeamento dos Endpoints para Cargos
app.MapPost("/AdicionarCargo", CargosEP.AdicionarCargo).WithName("AdicionarCargo");
app.MapPut("/AtualizarCargo", CargosEP.AtualizarCargo).WithName("AtualizarCargo");
app.MapDelete("/ExcluirCargo/{id}", CargosEP.ExcluirCargoById).WithName("ExcluirCargoById");
app.MapGet("/ListarTodos", CargosEP.ListarTodosCargos).WithName("ListarCargos");
app.MapGet("/BuscarPorID/{id}", CargosEP.BuscarPorId).WithName("BuscarCargoPorId");
app.MapGet("/BuscarPorPontos/Pontos", CargosEP.BuscarPorPontos).WithName("BuscarCargoPorPontos");
app.MapGet("/BuscarPorNome/Nome", CargosEP.BuscarPorNome).WithName("BuscarCargoPorNome");

// Mapeamento dos Endpoints para Categorias
app.MapPost("/AdicionarCategoria", CategoriasEP.AdicionarCategoria).WithName("AdicionarCategoria");
app.MapPut("/AtualizarCategoria", CategoriasEP.AtualizarCategoria).WithName("AtualizarCategoria");
app.MapDelete("/ExcluirCategoria/{id}", CategoriasEP.ExcluirCategoriaById).WithName("ExcluirCategoriaById");
app.MapGet("/ListarTodasCategorias", CategoriasEP.ListarTodasCategorias).WithName("ListarCategorias");
app.MapGet("/BuscarCategoriaPorID/{id}", CategoriasEP.BuscarPorId).WithName("BuscarCategoriaPorId");
app.MapGet("/BuscarCategoriaPorNome/Nome", CategoriasEP.BuscarPorNome).WithName("BuscarCategoriaPorNome");

// Mapeamento dos Endpoints para Clientes
app.MapPost("/AdicionarCliente", ClientesEP.AdicionarCliente).WithName("AdicionarCliente");
app.MapPut("/AtualizarCliente", ClientesEP.AtualizarCliente).WithName("AtualizarCliente");
app.MapDelete("/ExcluirCliente/{id}", ClientesEP.ExcluirClienteById).WithName("ExcluirClienteById");
app.MapGet("/ListarTodosClientes", ClientesEP.ListarTodosClientes).WithName("ListarTodosClientes");
app.MapGet("/BuscarClientePorId/{id}", ClientesEP.BuscarPorId).WithName("BuscarClientePorId");
app.MapGet("/BuscarClientePorNome/Nome", ClientesEP.BuscarPorNome).WithName("BuscarClientePorNome");
app.MapGet("/PesquisarClientesPorCriterio", ClientesEP.PesquisarClientesPorCriterio).WithName("PesquisarClientesPorCriterio");

// Mapeamento dos Endpoints para Cofres
app.MapPost("/AdicionarCofres", CofresEP.AdicionarCofre).WithName("AdicionarCofre");
app.MapPut("/AtualizarCofres", CofresEP.AtualizarCofre).WithName("AtualizarCofre");
app.MapDelete("/ExcluirCofre/{id}", CofresEP.ExcluirCofre).WithName("ExcluirCofre");
app.MapGet("/ListarCofres", CofresEP.ListarCofres).WithName("ListarCofres");
app.MapGet("/BuscarCofresPorID/{id}", CofresEP.BuscarCofrePorId).WithName("BuscarCofrePorId");

// Mapeamento dos Endpoints para Contatos
app.MapPost("/AdicionarContato", ContatosEP.AdicionarContato).WithName("AdicionarContato");
app.MapPut("/AtualizarContato", ContatosEP.AtualizarContato).WithName("AtualizarContato");
app.MapDelete("/ExcluirContato/{contatoId}", ContatosEP.ExcluirContato).WithName("ExcluirContato");
app.MapGet("/ListarContatos", ContatosEP.ListarContatos).WithName("ListarContatos");
app.MapGet("/BuscarContatoPorId/{id}", ContatosEP.BuscarContatoPorId).WithName("BuscarContatoPorId");
app.MapGet("/PesquisarContatosPorCriterio", ContatosEP.PesquisarContatosPorCriterio).WithName("PesquisarContatosPorCriterio");

// Mapeamento dos Endpoints para ControleGov
app.MapPost("/AdicionarControleGov", ControleGovEP.AdicionarControleGov).WithName("AdicionarControleGov");
app.MapPut("/AtualizarControleGov", ControleGovEP.AtualizarControleGov).WithName("AtualizarControleGov");
app.MapDelete("/ExcluirControleGov/{controleID}", ControleGovEP.ExcluirControleGov).WithName("ExcluirControleGov");
app.MapGet("/BuscarControleGovPorID/{controleID}", ControleGovEP.BuscarControleGovPorID).WithName("BuscarControleGovPorID");
app.MapGet("/ListarControleGov", ControleGovEP.ListarControleGov).WithName("ListarControleGov");
//app.MapGet("/ListarControleGovComFiltros", ControleGovEP.ListarControleGovComFiltros).WithName("ListarControleGovComFiltros");

// Mapeamento dos Endpoints para Fotos
app.MapPost("/AdicionarFotos", FotosEP.AdicionarFotos).WithName("AdicionarFotos");
app.MapDelete("/ExcluirFoto/{id}", FotosEP.ExcluirFoto).WithName("ExcluirFoto");
app.MapGet("/BuscarFotoPorId/{id}", FotosEP.BuscarFotoPorId).WithName("BuscarFotoPorId");
app.MapGet("/ListarFotos", FotosEP.ListarFotos).WithName("ListarFotos");
app.MapGet("/ListarFotosDaOrdemDeServico/{ordemDeServicoId}", FotosEP.ListarFotosDaOrdemDeServico).WithName("ListarFotosDaOrdemDeServico");

// Mapeamento dos Endpoints para Funcionarios
app.MapPost("/AdicionarFuncionario", FuncionariosEP.AdicionarFuncionario).WithName("AdicionarFuncionario");
app.MapPut("/AtualizarFuncionario", FuncionariosEP.AtualizarFuncionario).WithName("AtualizarFuncionario");
app.MapDelete("/ExcluirFuncionario/{id}", FuncionariosEP.ExcluirFuncionario).WithName("ExcluirFuncionario");
app.MapGet("/BuscarFuncionarioPorId/{id}", FuncionariosEP.BuscarFuncionarioPorId).WithName("BuscarFuncionarioPorId");
app.MapGet("/ListarFuncionarios/{usuarioAtivo}", FuncionariosEP.ListarFuncionarios).WithName("ListarFuncionarios");
app.MapGet("/PesquisarFuncionariosPorCriterio/{criterio}/{valorPesquisa}", FuncionariosEP.PesquisarFuncionariosPorCriterio).WithName("PesquisarFuncionariosPorCriterio");

// Mapeamento dos endpoints de ManutencaoEP
app.MapPost("/AdicionarManutencao", ManutencaoEP.AdicionarManutencao);
app.MapPut("/AtualizarManutencao", ManutencaoEP.AtualizarManutencao);
app.MapDelete("/ExcluirManutencao/{id}", ManutencaoEP.ExcluirManutencao);
app.MapGet("/BuscarManutencaoPorId/{id}", ManutencaoEP.BuscarManutencaoPorId);
app.MapGet("/ListarManutencoes", ManutencaoEP.ListarManutencoes);
app.MapGet("/PesquisarManutencoesPorCriterio/{criterio}/{valorPesquisa}", ManutencaoEP.PesquisarManutencoesPorCriterio);
app.MapGet("/RelatorioManutencao", ManutencaoEP.RelatorioManutencao);
app.MapGet("/RelatorioManutencao2", ManutencaoEP.RelatorioManutencao2);

// Mapeamento dos endpoints de OpcoesEP
app.MapPost("/AdicionarOpcoes", OpcoesEP.AdicionarOpcoes);
app.MapGet("/ObterOpcoes", OpcoesEP.ObterOpcoes);

// Mapeamento dos endpoints de Ordens de serviço
app.MapPost("/AdicionarOrdemDeServico", OrdemDeServicoEP.AdicionarOrdemDeServico);
app.MapPut("/AtualizarOrdemDeServico", OrdemDeServicoEP.AtualizarOrdemDeServico);
app.MapDelete("/ExcluirOrdemDeServico/{ordemDeServicoId}", OrdemDeServicoEP.ExcluirOrdemDeServico);
app.MapGet("/BuscarOrdemDeServicoPorId/{id}", OrdemDeServicoEP.BuscarOrdemDeServicoPorId);
app.MapGet("/ListarOrdensDeServico", OrdemDeServicoEP.ListarOrdensDeServico);
app.MapGet("/PesquisarOrdensDeServicoPorCriterio", OrdemDeServicoEP.PesquisarOrdensDeServicoPorCriterio);

// Configura o mapeamento dos endpoints definidos em PatrimoniosEP
app.MapPost("/AdicionarPatrimonio", PatrimoniosEP.AdicionarPatrimonio);
app.MapPut("/AtualizarPatrimonio", PatrimoniosEP.AtualizarPatrimonio);
app.MapPut("/AtualizarPatrimonioSemFoto", PatrimoniosEP.AtualizarPatrimonioSemFoto);
app.MapDelete("/ExcluirPatrimonio/{id}", PatrimoniosEP.ExcluirPatrimonio);
app.MapGet("/BuscarPatrimonioPorId/{id}", PatrimoniosEP.BuscarPatrimonioPorId);
app.MapGet("/ListarPatrimonios", PatrimoniosEP.ListarPatrimonios);
app.MapGet("/PesquisarPatrimoniosPorCriterio/{criterio}/{valorPesquisa}", PatrimoniosEP.PesquisarPatrimoniosPorCriterio);
app.MapGet("/RelatorioPatrimonios", PatrimoniosEP.RelatorioPatrimonios);
app.MapGet("/RelatorioPatrimonios2", PatrimoniosEP.RelatorioPatrimonios2);

// Configura o mapeamento dos endpoints definidos em PermissaoMenuEP
app.MapPost("/AdicionarPermissaoMenu", PermissaoMenuEP.AdicionarPermissaoMenu);
app.MapPut("/AtualizarPermissaoMenu", PermissaoMenuEP.AtualizarPermissaoMenu);
app.MapDelete("/RemoverPermissaoMenu/{usuarioId}/{menuOpcaoId}", PermissaoMenuEP.RemoverPermissaoMenu);
app.MapGet("/ObterPermissaoMenu/{usuarioId}/{menuOpcaoId}", PermissaoMenuEP.ObterPermissaoMenu);
app.MapGet("/ObterPermissoesPorUsuario/{usuarioId}", PermissaoMenuEP.ObterPermissoesPorUsuario);
app.MapGet("/ObterOpcoesMenuExceto/{tipo}", PermissaoMenuEP.ObterOpcoesMenuExceto);

// Configura o mapeamento dos endpoints definidos em SenhasEP
app.MapPost("/AdicionarSenha", SenhasEP.AdicionarSenha);
app.MapPut("/AtualizarSenha", SenhasEP.AtualizarSenha);
app.MapDelete("/ExcluirSenha/{senhaId}", SenhasEP.ExcluirSenha);
app.MapGet("/BuscarSenhaPorId/{id}", SenhasEP.BuscarSenhaPorId);
app.MapGet("/ListarSenhas", SenhasEP.ListarSenhas);
app.MapGet("/PesquisarSenhasPorCriterio", SenhasEP.PesquisarSenhasPorCriterio); // Sem parâmetros de rota

// Configura o mapeamento dos endpoints definidos em SetoresEP
app.MapPost("/AdicionarSetor", SetoresEP.AdicionarSetor);
app.MapPut("/AtualizarSetor", SetoresEP.AtualizarSetor);
app.MapDelete("/ExcluirSetor/{setorId}", SetoresEP.ExcluirSetor);
app.MapGet("/BuscarSetorPorId/{id}", SetoresEP.BuscarSetorPorId);
app.MapGet("/BuscarSetorPorNome", SetoresEP.BuscarSetorPorNome);
app.MapGet("/ListarSetores", SetoresEP.ListarSetores);

// Configura o mapeamento dos endpoints definidos em SubCategoriasEP
app.MapPost("/AdicionarSubcategoria", SubCategoriasEP.AdicionarSubcategoria);
app.MapPut("/AtualizarSubcategoria", SubCategoriasEP.AtualizarSubcategoria);
app.MapDelete("/ExcluirSubcategoria/{subcategoriaId}", SubCategoriasEP.ExcluirSubcategoria);
app.MapGet("/BuscarSubcategoriaPorId/{id}", SubCategoriasEP.BuscarSubcategoriaPorId);
app.MapGet("/PesquisarSubcategoriasPorCriterio", SubCategoriasEP.PesquisarSubcategoriasPorCriterio);
app.MapGet("/ListarSubcategorias", SubCategoriasEP.ListarSubcategorias);
app.MapGet("/ListarSubcategoriasPorIDCategoria", SubCategoriasEP.ListarSubcategoriasPorIDCategoria);

// Configura o mapeamento dos endpoints definidos em UsuariosEP
app.MapPost("/AdicionarUsuario", UsuariosEP.AdicionarUsuario);
app.MapPut("/AtualizarUsuario", UsuariosEP.AtualizarUsuario);
app.MapPut("/AtualizarUsuarioSemSenha", UsuariosEP.AtualizarUsuarioSemSenha);
app.MapPut("/AlterarSenha", UsuariosEP.AlterarSenha);
app.MapDelete("/ExcluirUsuario/{id}", UsuariosEP.ExcluirUsuario);
app.MapGet("/BuscarUsuarioPorId/{id}", UsuariosEP.BuscarUsuarioPorId);
app.MapGet("/BuscarUsuarioPorNome/{nome}", UsuariosEP.BuscarUsuarioPorNome);
app.MapGet("/ListarUsuarios", UsuariosEP.ListarUsuarios);
app.MapGet("/PesquisarUsuariosPorCriterio/{criterio}/{valorPesquisa}", UsuariosEP.PesquisarUsuariosPorCriterio);
app.MapGet("/VerificarAdministrador/{idUsuario}", UsuariosEP.VerificarAdministrador);

app.Run();
