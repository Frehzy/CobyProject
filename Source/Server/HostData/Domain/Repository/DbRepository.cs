using HostData.Domain.Context;
using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HostData.Domain.Repository;

public class DbRepository : IDbRepository
{
    private readonly DataContext _context;

    public DbRepository(DataContext context) =>
        _context = context;

    public IReadOnlyList<T> Get<T>() where T : class, IEntity
    {
        return _context.Set<T>().Where(x => x.IsActive).ToList();
    }

    public IReadOnlyList<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
    {
        return _context.Set<T>().Where(selector).Where(x => x.IsActive).ToList();
    }

    public IReadOnlyList<T> GetAll<T>() where T : class, IEntity
    {
        return _context.Set<T>().ToList();
    }

    public async Task<Guid> Add<T>(T newEntity) where T : class, IEntity
    {
        var entity = await _context.Set<T>().AddAsync(newEntity);
        return entity.Entity.Id;
    }

    public async Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity
    {
        await _context.Set<T>().AddRangeAsync(newEntities);
    }

    public async Task Delete<T>(Guid id) where T : class, IEntity
    {
        var activeEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        activeEntity.IsActive = false;
        await Task.Run(() => _context.Update(activeEntity));
    }

    public async Task Remove<T>(T entity) where T : class, IEntity
    {
        await Task.Run(() => _context.Set<T>().Remove(entity));
    }

    public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
    {
        await Task.Run(() => _context.Set<T>().RemoveRange(entities));
    }

    public async Task Update<T>(T entity) where T : class, IEntity
    {
        await Task.Run(() => _context.Set<T>().Update(entity));
    }

    public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity
    {
        await Task.Run(() => _context.Set<T>().UpdateRange(entities));
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}