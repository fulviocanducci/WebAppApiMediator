using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppApi.DataAccess;

namespace WebAppApi.Application.Infra.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DbSet<T> _model;
        private readonly DatabaseContext _context;
        public Repository(DatabaseContext context)
        {
            _context = context;
            _model = _context.Set<T>();
        }

        public async Task AddAsync(T model)
        {
            await _model.AddAsync(model);
        }

        public async Task<int> CommitAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IAsyncEnumerable<T> GetAsync()
        {
            return _model.AsNoTracking().AsAsyncEnumerable();
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await _model.FindAsync(keys);
        }

        public async Task RemoveAsync(params object[] keys)
        {
            var model = await GetAsync(keys);
            await RemoveAsync(model);
        }

        public Task RemoveAsync(T model)
        {
            _model.Remove(model);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T model)
        {
            _model.Update(model);
            return Task.CompletedTask;
        }
    }
}
