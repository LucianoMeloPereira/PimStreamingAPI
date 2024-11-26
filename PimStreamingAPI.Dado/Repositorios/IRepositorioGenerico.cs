﻿using System.Linq.Expressions;

namespace PimStreamingAPI.Dado.Repositorios
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task<T> AdicionarAsync(T entity); // Agora retorna a entidade adicionada
        Task AtualizarAsync(T entity);
        Task RemoverAsync(T entity);
        Task<IEnumerable<T>> ObterComFiltroAsync(Expression<Func<T, bool>> predicate);
    }
}
