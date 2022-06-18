﻿using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Mapper;
using HostData.Repository;
using Serilog;
using System.Text.Json;

namespace HostData.Services;

public abstract class BaseService
{
    protected IDbRepository DbRepository { get; }

    protected IMapper Mapper { get; }

    public BaseService(IDbRepository dbRepository,
                       IMapper mapper)
    {
        DbRepository = dbRepository;
        Mapper = mapper;
    }

    protected virtual async Task<Guid> Create<TModel, TEntity>(Guid entityThatChangesId, TModel model) where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var entity = Mapper.Map<TModel, TEntity>(model);
        entity.CreatedTime = DateTime.Now;
        entity.WaiterCreatedId = entityThatChangesId;
        entity.Version = 1;
        entity.IsDeleted = false;

        var result = await DbRepository.Add(entity);

        await DbRepository.SaveChangesAsync();

        Logging(OperationType.Added, result).ConfigureAwait(false);
        return result;
    }

    protected virtual async Task Delete<TEntity>(Guid id) where TEntity : class, IEntity, new()
    {
        await DbRepository.Delete<TEntity>(id);
        await DbRepository.SaveChangesAsync();
        Logging(OperationType.Delete, id).ConfigureAwait(false);
    }

    protected virtual async Task Update<TModel, TEntity>(Guid entityThatChangesId, TModel newModel) where TEntity : class, IEntity, new() where TModel : class, IModel, new()
    {
        var oldModel = await GetById<TModel, TEntity>(newModel.Id);

        var entity = Mapper.Map<TModel, TEntity>(newModel);
        entity.WaiterUpdatedId = entityThatChangesId;
        entity.UpdateTime = DateTime.Now;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
        Logging(oldModel, newModel).ConfigureAwait(false);
    }

    protected virtual async Task<TModel> GetById<TModel, TEntity>(Guid id) where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var entity = await DbRepository.GetById<TEntity>(id);
        var model = Mapper.Map<TEntity, TModel>(entity);
        return model;
    }

    protected virtual TModel Map<TModel, TEntity>(TEntity entity) where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var model = Mapper.Map<TEntity, TModel>(entity);
        return model;
    }

    protected virtual async Task<List<TModel>> GetAll<TModel, TEntity>() where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var collection = await DbRepository.GetAll<TEntity>();
        var models = Mapper.Map<TEntity, TModel>(collection);
        return models.ToList();
    }

    protected virtual async Task Remove<TEntity>(Guid entityThatChangesId, Guid id) where TEntity : class, IEntity
    {
        var entity = await DbRepository.GetById<TEntity>(id);
        entity.WaiterUpdatedId = entityThatChangesId;
        entity.UpdateTime = DateTime.Now;
        entity.IsDeleted = true;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
        Logging(OperationType.Remove, entity).ConfigureAwait(false);
    }

    protected virtual async Task Remove<TEntity>(Guid entityThatChangesId, TEntity entity) where TEntity : class, IEntity
    {
        entity.WaiterUpdatedId = entityThatChangesId;
        entity.UpdateTime = DateTime.Now;
        entity.IsDeleted = true;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
        Logging(OperationType.Remove, entity).ConfigureAwait(false);
    }

    private async Task Logging(OperationType operationType, Guid id) =>
        Log.Information($"{operationType} with Id [{id}]");

    private async Task Logging<TEntity>(OperationType operationType, TEntity entity) =>
        Log.Information($"{operationType}. {JsonSerializer.Serialize(entity, Json.Options.JsonSerializerOptions)}");

    private async Task Logging<TEntity>(TEntity oldItem, TEntity newItem) =>
        Log.Information($"{OperationType.Update}. " +
            $"OldItem: {JsonSerializer.Serialize(oldItem, Json.Options.JsonSerializerOptions)}\n" +
            $"NewItem: {JsonSerializer.Serialize(newItem, Json.Options.JsonSerializerOptions)}");

    private enum OperationType
    {
        Added,
        Update,
        Remove,
        Delete
    }
}