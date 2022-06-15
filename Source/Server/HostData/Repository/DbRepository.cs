﻿using HostData.Domain.Context;
using HostData.Domain.Contracts.Entities;
using HostData.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HostData.Domain.Repository;

public sealed class DbRepository : IDbRepository
{
    public DataContext Context { get; private set; }

    public DbRepository(DataContext context) =>
        Context = context;

    public async Task<T> GetById<T>(Guid id) where T : class, IEntity =>
        await Context.Set<T>().FirstAsync(x => x.Id.Equals(id));

    public async Task<List<T>> Get<T>() where T : class, IEntity =>
        await Context.Set<T>().Where(x => x.IsDeleted == false).ToListAsync();

    public async Task<List<T>> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity =>
        await Context.Set<T>().Where(selector).Where(x => x.IsDeleted == false).ToListAsync();

    public async Task<List<T>> GetAll<T>() where T : class, IEntity =>
        await Context.Set<T>().ToListAsync();

    public async Task<Guid> Add<T>(T newEntity) where T : class, IEntity
    {
        var entity = await Context.Set<T>().AddAsync(newEntity);
        return entity.Entity.Id;
    }

    public async Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity
    {
        await Context.Set<T>().AddRangeAsync(newEntities);
    }

    public async Task Delete<T>(Guid id) where T : class, IEntity
    {
        var activeEntity = await Context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        activeEntity.IsDeleted = true;
        await Task.Run(() => Context.Update(activeEntity));
    }

    public async Task Remove<T>(T entity) where T : class, IEntity
    {
        await Task.Run(() => Context.Set<T>().Remove(entity));
    }

    public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
    {
        await Task.Run(() => Context.Set<T>().RemoveRange(entities));
    }

    public async Task Update<T>(T entity) where T : class, IEntity
    {
        await Task.Run(() => Context.Set<T>().Update(entity));
    }

    public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity
    {
        await Task.Run(() => Context.Set<T>().UpdateRange(entities));
    }

    public async Task<int> SaveChangesAsync() =>
        await Context.SaveChangesAsync();
}