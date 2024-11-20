using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Servico.Servicos
{
    public class UsuarioServico : BaseServico<Usuario>, IUsuarioServico
    {
        private readonly IRepositorioGenerico<Usuario> _repositorio;

        public UsuarioServico(IRepositorioGenerico<Usuario> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            var usuarios = await _repositorio.ObterComFiltroAsync(u => u.Email == email);
            return usuarios.FirstOrDefault();
        }
    }
}
