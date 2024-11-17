using Microsoft.EntityFrameworkCore;
using PimStreamingAPI.Dado.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dado.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly AppDbContext _contexto;

        public RepositorioGenerico(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<T>> ObterTodosAsync()
        {
            return await _contexto.Set<T>().ToListAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public async Task AdicionarAsync(T entidade)
        {
            await _contexto.Set<T>().AddAsync(entidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(T entidade)
        {
            _contexto.Set<T>().Update(entidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var entidade = await ObterPorIdAsync(id);
            _contexto.Set<T>().Remove(entidade);
            await _contexto.SaveChangesAsync();
        }
    }
}
