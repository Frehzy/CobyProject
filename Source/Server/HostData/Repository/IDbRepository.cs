using HostData.Domain.Context;
using HostData.Domain.Contracts.Entities;
using System.Linq.Expressions;

namespace HostData.Repository;

public interface IDbRepository
{
    DataContext Context { get; }

    Task<T> GetById<T>(Guid id) where T : class, IEntity;
    Task<List<T>> Get<T>() where T : class, IEntity;
    Task<List<T>> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
    Task<List<T>> GetAll<T>() where T : class, IEntity;

    Task<Guid> Add<T>(T entity) where T : class, IEntity;
    Task AddRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task Delete<T>(Guid id) where T : class, IEntity;

    Task Remove<T>(T entity) where T : class, IEntity;
    Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task Update<T>(T entity) where T : class, IEntity;
    Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity;

    Task<bool> CheckIfExists<T>(T entity, Expression<Func<T, bool>> anyPredicate) where T : class, IEntity;

    Task<int> SaveChangesAsync();
}