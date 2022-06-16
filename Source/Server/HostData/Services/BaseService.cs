using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public abstract class BaseService
{
    protected IDbRepository DbRepository { get; }

    protected IMapper Mapper { get; }

    protected OrderWaiterEntity ConnectEntity { get; } //объект, который пытается изменить БД

    public BaseService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity)
    {
        DbRepository = dbRepository;
        Mapper = mapper;
        ConnectEntity = connectEntity;
    }

    protected virtual async Task<Guid> Create<TModel, TEntity>(TModel model) where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var entity = Mapper.Map<TModel, TEntity>(model);
        entity.CreatedTime = DateTime.Now;
        entity.WaiterCreatedId = ConnectEntity.Id;
        entity.Version = 1;
        entity.IsDeleted = false;

        var result = await DbRepository.Add(entity);

        await DbRepository.SaveChangesAsync();

        return result;
    }

    protected virtual async Task Delete<TEntity>(Guid id) where TEntity : class, IEntity, new()
    {
        await DbRepository.Delete<TEntity>(id);
        await DbRepository.SaveChangesAsync();
    }

    protected virtual async Task Update<TModel, TEntity>(TModel model) where TEntity : class, IEntity, new() where TModel : class, new()
    {
        var entity = Mapper.Map<TModel, TEntity>(model);
        entity.WaiterUpdatedId = ConnectEntity.Id;
        entity.UpdateTime = DateTime.Now;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
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

    protected virtual async Task Remove<TEntity>(Guid id) where TEntity : class, IEntity
    {
        var entity = await DbRepository.GetById<TEntity>(id);
        entity.WaiterUpdatedId = ConnectEntity.Id;
        entity.UpdateTime = DateTime.Now;
        entity.IsDeleted = true;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
    }

    protected virtual async Task Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        entity.WaiterUpdatedId = ConnectEntity.Id;
        entity.UpdateTime = DateTime.Now;
        entity.IsDeleted = true;

        await DbRepository.Update(entity);
        await DbRepository.SaveChangesAsync();
    }
}