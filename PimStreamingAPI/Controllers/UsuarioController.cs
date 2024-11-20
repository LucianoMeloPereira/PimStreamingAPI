using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;
using PimStreamingAPI.Dominio.DTO.UsuarioDTO;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        // Endpoint para criar um novo usuário
        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<UsuarioResponseDTO>> CriarUsuario([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            // Mapeia o DTO para a entidade de domínio
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Sobrenome = usuarioDTO.Sobrenome,
                Idade = usuarioDTO.Idade,
                Telefone = usuarioDTO.Telefone,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha
            };

            await _usuarioServico.AdicionarAsync(usuario);

            // Mapeia a entidade de domínio para o DTO de resposta
            var usuarioResponse = new UsuarioResponseDTO
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Telefone = usuario.Telefone
            };

            return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = usuario.ID }, usuarioResponse);
        }

        // Endpoint para obter todos os usuários
        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> ObterTodos()
        {
            var usuarios = await _usuarioServico.ObterTodosAsync();

            // Converte a lista de entidades para a lista de DTOs
            var usuariosResponse = usuarios.Select(u => new UsuarioResponseDTO
            {
                ID = u.ID,
                Nome = u.Nome,
                Sobrenome = u.Sobrenome,
                Email = u.Email,
                Idade = u.Idade,
                Telefone = u.Telefone
            });

            return Ok(usuariosResponse);
        }

        // Endpoint para obter um usuário por ID
        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioServico.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            // Converte a entidade para o DTO de resposta
            var usuarioResponse = new UsuarioResponseDTO
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Telefone = usuario.Telefone
            };

            return Ok(usuarioResponse);
        }

        // Endpoint para atualizar um usuário
        [HttpPut("AtualizarUsuario/{id}")]
        public async Task<ActionResult> AtualizarUsuario(int id, [FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                ID = id,
                Nome = usuarioDTO.Nome,
                Sobrenome = usuarioDTO.Sobrenome,
                Idade = usuarioDTO.Idade,
                Telefone = usuarioDTO.Telefone,
                Email = usuarioDTO.Email
            };

            await _usuarioServico.AtualizarAsync(usuario);

            return NoContent();
        }

        // Endpoint para deletar um usuário
        [HttpDelete("DeletarUsuario/{id}")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            await _usuarioServico.RemoverAsync(id);
            return NoContent();
        }
    }
}
