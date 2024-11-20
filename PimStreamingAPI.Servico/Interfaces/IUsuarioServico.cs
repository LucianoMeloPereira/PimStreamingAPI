using PimStreamingAPI.Dominio.Entidades;

namespace PimStreamingAPI.Servico.Interfaces
{
    public interface IUsuarioServico : IBaseServico<Usuario>
    {
        Task<Usuario> ObterPorEmailAsync(string email);
    }
}
