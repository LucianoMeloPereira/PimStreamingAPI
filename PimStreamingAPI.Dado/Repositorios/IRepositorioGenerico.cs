using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dado.Repositorios
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task AdicionarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task DeletarAsync(int id);
    }
}
