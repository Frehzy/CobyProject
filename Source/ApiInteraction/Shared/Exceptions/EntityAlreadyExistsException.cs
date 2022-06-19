using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

[Serializable]
public class EntityAlreadyExistsException : EntityException
{
    public string EntityType { get; set; }

    public override string Message => ToString();

    public EntityAlreadyExistsException() : base(default, nameof(EntityAlreadyExistsException)) { }

    public EntityAlreadyExistsException(Guid id, string entityType) : base(id, nameof(EntityAlreadyExistsException))
    {
        EntityType = entityType;
    }

    public EntityAlreadyExistsException(Guid id, string entityType, ApiException innerException) : base(id, nameof(EntityAlreadyExistsException), innerException)
    {
        EntityType = entityType;
    }

    protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        EntityType = (string)info.GetValue(nameof(EntityType), typeof(string));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        dic.Add(nameof(EntityType), EntityType);

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(EntityType), Message, typeof(string));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());
        strBuilder.AppendFormat(string.Format(@"EntityType: [{0}]. ", EntityType), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        EntityType = default;
    }
}