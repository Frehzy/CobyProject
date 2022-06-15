namespace HostData.Domain.Contracts.Services;

public interface IBaseService<T> where T : class
{
    Task<Guid> Create(T model);

    Task<T> Get(Guid id);

    Task<List<T>> GetAll();

    Task Update(T model);

    Task Delete(Guid id);

    Task Remove(Guid id);
}