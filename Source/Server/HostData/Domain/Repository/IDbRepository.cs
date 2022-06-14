using HostData.Domain.Contracts.Entities;
using System.Linq.Expressions;

namespace HostData.Domain.Repository;

public interface IDbRepository
{
    IReadOnlyList<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
    IReadOnlyList<T> Get<T>() where T : class, IEntity;
    IReadOnlyList<T> GetAll<T>() where T : class, IEntity;

    Task<Guid> Add<T>(T entity) where T : class, IEntity;
    Task AddRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task Delete<T>(Guid entity) where T : class, IEntity;

    Task Remove<T>(T entity) where T : class, IEntity;
    Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task Update<T>(T entity) where T : class, IEntity;
    Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task<int> SaveChangesAsync();
}