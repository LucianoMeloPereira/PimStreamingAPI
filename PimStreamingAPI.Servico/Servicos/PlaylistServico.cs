using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Dominio.DTO.PlayListDTO;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Servico.Servicos
{
    public class PlaylistServico : BaseServico<Playlist>, IPlaylistServico
    {
        private readonly IRepositorioGenerico<Playlist> _repositorio;
        private readonly IRepositorioGenerico<Usuario> _usuarioRepositorio;

        public PlaylistServico(IRepositorioGenerico<Playlist> repositorio, IRepositorioGenerico<Usuario> usuarioRepositorio)
            : base(repositorio)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IEnumerable<Playlist>> ObterPlaylistsPorUsuarioIdAsync(int usuarioId)
        {
            return await _repositorio.ObterComFiltroAsync(p => p.UsuarioID == usuarioId);
        }

        public async Task<PlaylistResponseDTO> CriarPlaylistAsync(PlaylistCreateDTO playlistDTO)
        {
            // Verifica se o usuário existe
            var usuario = await _usuarioRepositorio.ObterPorIdAsync(playlistDTO.UsuarioID);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            // Cria a nova playlist
            var novaPlaylist = new Playlist
            {
                Nome = playlistDTO.Nome,
                UsuarioID = playlistDTO.UsuarioID
            };

            await _repositorio.AdicionarAsync(novaPlaylist);

            return new PlaylistResponseDTO
            {
                ID = novaPlaylist.ID,
                Nome = novaPlaylist.Nome,
                UsuarioID = novaPlaylist.UsuarioID
            };
        }
    }
}
