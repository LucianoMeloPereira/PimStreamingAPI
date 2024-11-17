using PimStreamingAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Servico.Interfaces
{
    public interface IUsuarioServico
    {
        Task<List<Usuario>> ObterTodosUsuariosAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int id);
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task DeletarUsuarioAsync(int id);
    }
}
