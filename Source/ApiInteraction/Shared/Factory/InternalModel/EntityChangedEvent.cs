using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class EntityChangedEvent<TEntity> : IEntityChangedEvent<TEntity> where TEntity : class
{
    public TEntity Entity { get; set; }

    public EventType EventType { get; set; }

    public EntityChangedEvent() { }

    public EntityChangedEvent(TEntity entity, EventType eventType)
    {
        Entity = entity;
        EventType = eventType;
    }
}