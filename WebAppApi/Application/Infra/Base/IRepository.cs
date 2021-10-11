using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppApi.Application.Infra.Base
{
    public interface IRepository<T> where T : class, new()
    {
        Task AddAsync(T model);
        Task UpdateAsync(T model);
        IAsyncEnumerable<T> GetAsync();
        Task<T> GetAsync(params object[] keys);
        Task RemoveAsync(params object[] keys);
        Task RemoveAsync(T model);
        Task<int> CommitAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}
