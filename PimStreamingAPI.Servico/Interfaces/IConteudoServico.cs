using PimStreamingAPI.Dominio.Entidades;

namespace PimStreamingAPI.Servico.Interfaces
{
    public interface IConteudoServico : IBaseServico<Conteudo>
    {
        Task<IEnumerable<Conteudo>> ObterConteudosPorPlaylistIdAsync(int playlistId);
        Task<IEnumerable<Conteudo>> ObterConteudosPorCriadorIdAsync(int criadorId);
    }
}
