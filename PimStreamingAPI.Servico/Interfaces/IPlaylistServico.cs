using PimStreamingAPI.Dominio.DTO.PlayListDTO;
using PimStreamingAPI.Dominio.Entidades;

namespace PimStreamingAPI.Servico.Interfaces
{
    public interface IPlaylistServico : IBaseServico<Playlist>
    {
        Task<IEnumerable<Playlist>> ObterPlaylistsPorUsuarioIdAsync(int usuarioId);
        Task<PlaylistResponseDTO> CriarPlaylistAsync(PlaylistCreateDTO playlistDTO);
    }
}
