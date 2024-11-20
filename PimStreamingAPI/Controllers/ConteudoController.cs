using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.DTO.ConteudoDTO;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudoController : ControllerBase
    {
        private readonly IConteudoServico _conteudoServico;

        public ConteudoController(IConteudoServico conteudoServico)
        {
            _conteudoServico = conteudoServico;
        }

        [HttpPost("CriarConteudo")]
        public async Task<ActionResult<ConteudoResponseDTO>> CriarConteudo([FromBody] ConteudoCreateDTO conteudoDTO)
        {
            var novoConteudo = new Conteudo
            {
                Titulo = conteudoDTO.Titulo,
                Tipo = conteudoDTO.Tipo,
                PlaylistID = conteudoDTO.PlaylistID,
                CriadorID = conteudoDTO.CriadorID
            };

            await _conteudoServico.AdicionarAsync(novoConteudo);

            var response = new ConteudoResponseDTO
            {
                ID = novoConteudo.ID,
                Titulo = novoConteudo.Titulo,
                Tipo = novoConteudo.Tipo,
                PlaylistID = novoConteudo.PlaylistID,
                CriadorID = novoConteudo.CriadorID
            };

            return CreatedAtAction(nameof(ObterConteudoPorId), new { id = response.ID }, response);
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> ObterTodos()
        {
            var conteudos = await _conteudoServico.ObterTodosAsync();

            var response = conteudos.Select(c => new ConteudoResponseDTO
            {
                ID = c.ID,
                Titulo = c.Titulo,
                Tipo = c.Tipo,
                PlaylistID = c.PlaylistID,
                CriadorID = c.CriadorID
            });

            return Ok(response);
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<ConteudoResponseDTO>> ObterConteudoPorId(int id)
        {
            var conteudo = await _conteudoServico.ObterPorIdAsync(id);

            if (conteudo == null)
                return NotFound("Conteúdo não encontrado.");

            var response = new ConteudoResponseDTO
            {
                ID = conteudo.ID,
                Titulo = conteudo.Titulo,
                Tipo = conteudo.Tipo,
                PlaylistID = conteudo.PlaylistID,
                CriadorID = conteudo.CriadorID
            };

            return Ok(response);
        }

        [HttpGet("ObterPorPlaylist/{playlistId}")]
        public async Task<ActionResult<IEnumerable<ConteudoResponseDTO>>> ObterPorPlaylist(int playlistId)
        {
            var conteudos = await _conteudoServico.ObterConteudosPorPlaylistIdAsync(playlistId);

            var response = conteudos.Select(c => new ConteudoResponseDTO
            {
                ID = c.ID,
                Titulo = c.Titulo,
                Tipo = c.Tipo,
                PlaylistID = c.PlaylistID,
                CriadorID = c.CriadorID
            });

            return Ok(response);
        }

        [HttpPut("AtualizarConteudo/{id}")]
        public async Task<ActionResult> AtualizarConteudo(int id, [FromBody] ConteudoCreateDTO conteudoDTO)
        {
            var conteudo = new Conteudo
            {
                ID = id,
                Titulo = conteudoDTO.Titulo,
                Tipo = conteudoDTO.Tipo,
                PlaylistID = conteudoDTO.PlaylistID,
                CriadorID = conteudoDTO.CriadorID
            };

            await _conteudoServico.AtualizarAsync(conteudo);

            return NoContent();
        }

        [HttpDelete("DeletarConteudo/{id}")]
        public async Task<ActionResult> DeletarConteudo(int id)
        {
            await _conteudoServico.RemoverAsync(id);
            return NoContent();
        }
    }
}
