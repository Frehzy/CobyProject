using SharedData.Entities.Contract;

namespace ApiHostData.Services;

public interface IBaseService<TModel> where TModel : class, IModel
{
    Task<Guid> Create(Guid entityThatChangesId, TModel model);

    Task<TModel> GetById(Guid id);

    Task<List<TModel>> Get();

    Task Update(Guid entityThatChangesId, TModel model);

    Task Delete(Guid id);

    Task Remove(Guid entityThatChangesId, Guid id);
}