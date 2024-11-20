using System.Linq.Expressions;
using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Servico.Interfaces;

namespace PimStreamingAPI.Servico.Servicos
{
    public class BaseServico<T> : IBaseServico<T> where T : class
    {
        private readonly IRepositorioGenerico<T> _repositorio;

        public BaseServico(IRepositorioGenerico<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<T> AdicionarAsync(T entity)
        {
            await _repositorio.AdicionarAsync(entity);
            return entity; // Retorna a entidade adicionada
        }

        public async Task AtualizarAsync(T entity)
        {
            await _repositorio.AtualizarAsync(entity);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _repositorio.ObterTodosAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _repositorio.ObterPorIdAsync(id);
        }

        public async Task RemoverAsync(int id)
        {
            var entity = await _repositorio.ObterPorIdAsync(id);
            if (entity != null)
            {
                await _repositorio.RemoverAsync(entity);
            }
        }

        public async Task<IEnumerable<T>> ObterComFiltroAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repositorio.ObterComFiltroAsync(predicate);
        }
    }
}
