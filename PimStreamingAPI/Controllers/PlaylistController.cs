using Microsoft.AspNetCore.Mvc;
using PimStreamingAPI.Dominio.DTO.PlayListDTO;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistServico _playlistServico;

        public PlaylistController(IPlaylistServico playlistServico)
        {
            _playlistServico = playlistServico;
        }

        // Endpoint para criar uma playlist
        [HttpPost("CriarPlaylist")]
        public async Task<ActionResult<PlaylistResponseDTO>> CriarPlaylist([FromBody] PlaylistCreateDTO playlistDTO)
        {
            var novaPlaylist = await _playlistServico.CriarPlaylistAsync(playlistDTO);
            return CreatedAtAction(nameof(ObterPlaylistPorId), new { id = novaPlaylist.ID }, novaPlaylist);
        }

        // Endpoint para obter todas as playlists
        [HttpGet("ObterTodas")]
        public async Task<ActionResult<IEnumerable<PlaylistResponseDTO>>> ObterTodas()
        {
            var playlists = await _playlistServico.ObterTodosAsync();
            var response = playlists.Select(p => new PlaylistResponseDTO
            {
                ID = p.ID,
                Nome = p.Nome,
                UsuarioID = p.UsuarioID
            });
            return Ok(response);
        }

        // Endpoint para obter playlists de um usuário
        [HttpGet("ObterPorUsuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PlaylistResponseDTO>>> ObterPorUsuario(int usuarioId)
        {
            var playlists = await _playlistServico.ObterPlaylistsPorUsuarioIdAsync(usuarioId);
            if (!playlists.Any())
                return NotFound("Nenhuma playlist encontrada para este usuário.");

            var response = playlists.Select(p => new PlaylistResponseDTO
            {
                ID = p.ID,
                Nome = p.Nome,
                UsuarioID = p.UsuarioID
            });
            return Ok(response);
        }

        // Endpoint para obter uma playlist por ID
        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<PlaylistResponseDTO>> ObterPlaylistPorId(int id)
        {
            var playlist = await _playlistServico.ObterPorIdAsync(id);
            if (playlist == null)
                return NotFound("Playlist não encontrada.");

            var response = new PlaylistResponseDTO
            {
                ID = playlist.ID,
                Nome = playlist.Nome,
                UsuarioID = playlist.UsuarioID
            };
            return Ok(response);
        }

        // Endpoint para atualizar uma playlist
        [HttpPut("AtualizarPlaylist/{id}")]
        public async Task<ActionResult> AtualizarPlaylist(int id, [FromBody] PlaylistCreateDTO playlistDTO)
        {
            var playlist = await _playlistServico.ObterPorIdAsync(id);
            if (playlist == null)
                return NotFound("Playlist não encontrada.");

            playlist.Nome = playlistDTO.Nome;
            playlist.UsuarioID = playlistDTO.UsuarioID;

            await _playlistServico.AtualizarAsync(playlist);
            return NoContent();
        }

        // Endpoint para deletar uma playlist
        [HttpDelete("DeletarPlaylist/{id}")]
        public async Task<ActionResult> DeletarPlaylist(int id)
        {
            var playlist = await _playlistServico.ObterPorIdAsync(id);
            if (playlist == null)
                return NotFound("Playlist não encontrada.");

            await _playlistServico.RemoverAsync(id);
            return NoContent();
        }
    }
}
