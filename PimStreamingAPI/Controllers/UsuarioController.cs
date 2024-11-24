using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;
using PimStreamingAPI.Dominio.DTO.UsuarioDTO;
using PimStreamingAPI.Servico.Servicos;
using PimStreamingAPI.DTO;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly JwtTokenService _jwtTokenService;

        public UsuarioController(IUsuarioServico usuarioServico, JwtTokenService jwtTokenService)
        {
            _usuarioServico = usuarioServico;
            _jwtTokenService = jwtTokenService;
        }

        // Endpoint para criar um novo usuário
        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<UsuarioResponseDTO>> CriarUsuario([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Sobrenome = usuarioDTO.Sobrenome,
                Idade = usuarioDTO.Idade,
                Telefone = usuarioDTO.Telefone,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha // Certifique-se de criptografar essa senha antes de salvar
            };

            await _usuarioServico.AdicionarAsync(usuario);

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

        // Endpoint para login (autenticação JWT)
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Autenticação: Verifica e-mail e senha no banco
            var usuario = await _usuarioServico.ObterComFiltroAsync(u =>
                u.Email == loginDTO.Email && u.Senha == loginDTO.Senha);

            if (usuario == null || !usuario.Any())
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var user = usuario.First();

            // Gera o token JWT
            var token = _jwtTokenService.GenerateToken(userId: user.ID.ToString(), role: "Usuario");

            return Ok(new
            {
                Token = token,
                Message = "Login realizado com sucesso!"
            });
        }
    }
}
