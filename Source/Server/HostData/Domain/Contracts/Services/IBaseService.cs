namespace HostData.Domain.Contracts.Services;

public interface IBaseService<T> where T : class
{
    Task<Guid> Create(Guid entityThatChangesId, T model);

    Task<T> GetById(Guid id);

    Task<List<T>> GetAll();

    Task Update(Guid entityThatChangesId, T model);

    Task Delete(Guid id);

    Task Remove(Guid entityThatChangesId, Guid id);
}