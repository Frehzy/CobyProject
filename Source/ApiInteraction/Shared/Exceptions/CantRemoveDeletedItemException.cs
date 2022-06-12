using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

public class CantRemoveDeletedItemException : ViolationBusinessLogicException
{
    public Guid EntityId { get; set; }

    public override string Message => ToString();

    public CantRemoveDeletedItemException(Guid entityId) : base(nameof(CantRemoveDeletedItemException))
    {
        EntityId = entityId;
    }

    protected CantRemoveDeletedItemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        EntityId = (Guid)info.GetValue(nameof(EntityId), typeof(Guid));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        dic.Add(nameof(EntityId), EntityId);

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(EntityId), Message, typeof(Guid));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());
        strBuilder.AppendFormat(string.Format(@"EntityId: [{0}]. ", EntityId), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        EntityId = Guid.Empty;
    }
}