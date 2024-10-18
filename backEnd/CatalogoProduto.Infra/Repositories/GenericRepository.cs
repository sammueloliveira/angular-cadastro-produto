using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProduto.Infra.Repositories
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly Contexto _contexto;
        private bool _disposed = false;

        public GenericRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Add(T objeto)
        {
            await _contexto.Set<T>().AddAsync(objeto);
            await _contexto.SaveChangesAsync();
        }
        public async Task Update(T objeto)
        {
            _contexto.Set<T>().Update(objeto);
            await _contexto.SaveChangesAsync();
        }
        public async Task Delete(T objeto)
        {
            _contexto.Set<T>().Remove(objeto);
            await _contexto.SaveChangesAsync();
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> List()
        {
            return await _contexto.Set<T>().ToListAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
