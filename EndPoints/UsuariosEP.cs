using API_CONTROLE.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace API_CONTROLE.EndPoints
{
    public static class UsuariosEP
    {
        private static UsuariosET usuariosET = new UsuariosET();

        [HttpPost("/AdicionarUsuario")]
        [SwaggerOperation(Summary = "Adicionar usuário", Description = "Adiciona um novo usuário.")]
        public static IActionResult AdicionarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                usuario.Senha = UsuariosET.CriptografarSenha(usuario.Senha);
                usuariosET.AdicionarUsuario(usuario);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao adicionar usuário: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarUsuario")]
        [SwaggerOperation(Summary = "Atualizar usuário", Description = "Atualiza um usuário existente.")]
        public static IActionResult AtualizarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                usuariosET.AtualizarUsuario(usuario);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar usuário: " + ex.Message);
            }
        }

        [HttpPut("/AtualizarUsuarioSemSenha")]
        [SwaggerOperation(Summary = "Atualizar usuário sem senha", Description = "Atualiza um usuário existente sem alterar a senha.")]
        public static IActionResult AtualizarUsuarioSemSenha([FromBody] Usuarios usuario)
        {
            try
            {
                usuariosET.AtualizarUsuarioSemSenha(usuario);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao atualizar usuário sem senha: " + ex.Message);
            }
        }

        [HttpPut("/AlterarSenha")]
        [SwaggerOperation(Summary = "Alterar senha do usuário", Description = "Altera a senha de um usuário.")]
        public static IActionResult AlterarSenha([FromBody] Usuarios usuario)
        {
            try
            {
                usuario.Senha = UsuariosET.CriptografarSenha(usuario.Senha);
                bool sucesso = usuariosET.AlterarSenha(usuario);
                if (sucesso)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Erro ao alterar senha do usuário: " + ex.Message);
            }
        }

        [HttpDelete("/ExcluirUsuario/{id}")]
        [SwaggerOperation(Summary = "Excluir usuário", Description = "Exclui um usuário pelo ID.")]
        public static IActionResult ExcluirUsuario([FromRoute] int id)
        {
            bool sucesso = usuariosET.ExcluirUsuario(id);
            if (sucesso)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("/BuscarUsuarioPorId/{id}")]
        [SwaggerOperation(Summary = "Buscar usuário por ID", Description = "Busca um usuário pelo ID.")]
        public static IActionResult BuscarUsuarioPorId([FromRoute] int id)
        {
            var usuario = usuariosET.BuscarUsuarioPorId(id);
            if (usuario != null)
            {
                return new OkObjectResult(usuario);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("/BuscarUsuarioPorNome/{nome}")]
        [SwaggerOperation(Summary = "Buscar usuário por nome", Description = "Busca um usuário pelo nome.")]
        public static IActionResult BuscarUsuarioPorNome([FromRoute] string nome)
        {
            var usuario = usuariosET.BuscarUsuarioPorNome(nome);
            if (usuario != null)
            {
                return new OkObjectResult(usuario);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("/ListarUsuarios")]
        [SwaggerOperation(Summary = "Listar usuários", Description = "Lista todos os usuários.")]
        public static IActionResult ListarUsuarios()
        {
            var usuarios = usuariosET.ListarUsuarios();
            return new OkObjectResult(usuarios);
        }

        [HttpGet("/PesquisarUsuariosPorCriterio/{criterio}/{valorPesquisa}")]
        [SwaggerOperation(Summary = "Pesquisar usuários por critério", Description = "Pesquisa usuários por critério e valor de pesquisa.")]
        public static IActionResult PesquisarUsuariosPorCriterio([FromRoute] string criterio, [FromRoute] string valorPesquisa)
        {
            var usuarios = usuariosET.PesquisarUsuariosPorCriterio(criterio, valorPesquisa);
            return new OkObjectResult(usuarios);
        }

        [HttpGet("/VerificarAdministrador/{idUsuario}")]
        [SwaggerOperation(Summary = "Verificar se o usuário é administrador", Description = "Verifica se o usuário com o ID fornecido é um administrador.")]
        public static IActionResult VerificarAdministrador([FromRoute] int idUsuario)
        {
            bool isAdmin = usuariosET.VerificarAdministrador(idUsuario);
            return new OkObjectResult(isAdmin);
        }
    }
}
