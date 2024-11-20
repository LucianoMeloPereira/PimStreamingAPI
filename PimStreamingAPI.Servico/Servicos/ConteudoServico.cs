using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Servico.Servicos
{
    public class ConteudoServico : BaseServico<Conteudo>, IConteudoServico
    {
        private readonly IRepositorioGenerico<Conteudo> _repositorio;

        public ConteudoServico(IRepositorioGenerico<Conteudo> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Conteudo>> ObterConteudosPorPlaylistIdAsync(int playlistId)
        {
            return await _repositorio.ObterComFiltroAsync(c => c.PlaylistID == playlistId);
        }

        public async Task<IEnumerable<Conteudo>> ObterConteudosPorCriadorIdAsync(int criadorId)
        {
            return await _repositorio.ObterComFiltroAsync(c => c.CriadorID == criadorId);
        }
    }
}
