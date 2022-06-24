using Shared.Data.Enum;

namespace Shared.Data;

public interface IEntityChangedEvent<TEntity> where TEntity : class
{
    public TEntity Entity { get; }

    public EventType EventType { get; }
}