using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Dominio.Entidades;
using PimStreamingAPI.Servico.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Servico.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IRepositorioGenerico<Usuario> _repositorioUsuario;

        public UsuarioServico(IRepositorioGenerico<Usuario> repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<List<Usuario>> ObterTodosUsuariosAsync()
        {
            return await _repositorioUsuario.ObterTodosAsync();
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int id)
        {
            return await _repositorioUsuario.ObterPorIdAsync(id);
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
            await _repositorioUsuario.AdicionarAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            await _repositorioUsuario.AtualizarAsync(usuario);
        }

        public async Task DeletarUsuarioAsync(int id)
        {
            await _repositorioUsuario.DeletarAsync(id);
        }
    }
}
