namespace HostData.Cache;

public interface IBaseCache<T>
{
    IReadOnlyCollection<T> Values { get; }

    T GetById(Guid id);

    void AddOrUpdate(T instance);

    T RemoveById(Guid id);
}