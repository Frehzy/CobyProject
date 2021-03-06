using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

[Serializable]
public class InvalidSessionException : EntityException
{
    public int Version { get; set; }

    public override string Message => ToString();

    public InvalidSessionException() : base(default, nameof(InvalidSessionException)) { }

    public InvalidSessionException(int version, Guid orderId) : base(orderId, nameof(InvalidSessionException))
    {
        Version = version;
    }

    public InvalidSessionException(int version, Guid orderId, string message) : base(orderId, message)
    {
        Version = version;
    }

    public InvalidSessionException(int version, Guid orderId, string message, ApiException innerException) : base(orderId, message, innerException)
    {
        Version = version;
    }

    protected InvalidSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        Version = (int)info.GetValue(nameof(Version), typeof(int));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        dic.Add(nameof(Version), Version);

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Version), Message, typeof(int));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());
        strBuilder.AppendFormat(string.Format(@"Version: [{0}]. ", Version), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        Version = default;
    }
}