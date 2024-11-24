using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.Servico.Servicos;
using PimStreamingAPI.Servico.Interfaces;
using PimStreamingAPI.Dominio.DTO.UsuarioDTO;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _tokenService;
        private readonly IUsuarioServico _usuarioServico;

        public AuthController(JwtTokenService tokenService, IUsuarioServico usuarioServico)
        {
            _tokenService = tokenService;
            _usuarioServico = usuarioServico;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Verifica se o usuário existe no banco
            var usuario = await _usuarioServico.ObterComFiltroAsync(u =>
                u.Email == request.Username && u.Senha == request.Password);

            if (usuario == null || !usuario.Any())
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var user = usuario.First();

            // Gera o token JWT
            var token = _tokenService.GenerateToken(userId: user.ID.ToString(), role: "Usuario");

            return Ok(new
            {
                Token = token,
                Message = "Login realizado com sucesso!"
            });
        }
    }

    // DTO de requisição para login
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
