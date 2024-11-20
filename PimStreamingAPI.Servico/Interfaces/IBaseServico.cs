using System.Linq.Expressions;

namespace PimStreamingAPI.Servico.Interfaces
{
    public interface IBaseServico<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task<T> AdicionarAsync(T entity); // Alterado para retornar a entidade
        Task AtualizarAsync(T entity);
        Task RemoverAsync(int id);
        Task<IEnumerable<T>> ObterComFiltroAsync(Expression<Func<T, bool>> predicate);
    }
}
