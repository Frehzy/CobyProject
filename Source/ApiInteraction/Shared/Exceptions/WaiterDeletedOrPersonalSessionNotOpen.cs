using System.Runtime.Serialization;

namespace Shared.Exceptions;

[Serializable]
public class WaiterDeletedOrPersonalSessionNotOpen : EntityException
{
    public override string Message => ToString();

    public WaiterDeletedOrPersonalSessionNotOpen() : base(default, nameof(WaiterDeletedOrPersonalSessionNotOpen)) { }

    public WaiterDeletedOrPersonalSessionNotOpen(Guid id) 
        : base(id, nameof(WaiterDeletedOrPersonalSessionNotOpen))
    {
    }

    protected WaiterDeletedOrPersonalSessionNotOpen(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }

    public override Dictionary<string, object> CreateDictionary() =>
        base.CreateDictionary();

    public override void GetObjectData(SerializationInfo info, StreamingContext context) =>
        base.GetObjectData(info, context);

    public override string ToString() =>
        base.ToString();

    protected override void Init() =>
        base.Init();
}