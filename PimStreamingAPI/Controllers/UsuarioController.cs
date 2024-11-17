using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet("ObterTodosUsuarios")]
        public async Task<ActionResult<List<Usuario>>> ObterTodosUsuarios()
        {
            var usuarios = await _usuarioServico.ObterTodosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("ObterUsuarioPorId/{id}")]
        public async Task<ActionResult<Usuario>> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioServico.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<ActionResult> AdicionarUsuario([FromBody] Usuario usuario)
        {
            await _usuarioServico.AdicionarUsuarioAsync(usuario);
            return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = usuario.ID }, usuario);
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<ActionResult> AtualizarUsuario([FromBody] Usuario usuario)
        {
            await _usuarioServico.AtualizarUsuarioAsync(usuario);
            return NoContent();
        }

        [HttpDelete("DeletarUsuario/{id}")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            await _usuarioServico.DeletarUsuarioAsync(id);
            return NoContent();
        }
    }
}
