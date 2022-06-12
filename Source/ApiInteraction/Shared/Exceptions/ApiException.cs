using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

[Serializable]
public abstract class ApiException : Exception, ISerializable
{
    public DateTime TimeStamp { get; set; }

    public string CustomMessage { get; set; }

    public override string Message => ToString();

    public override string? StackTrace => base.StackTrace.Replace("\r\n", string.Empty);

    public ApiException() : base(nameof(ApiException))
        => Init();

    public ApiException(string message) : base(message)
    {
        Init();
        CustomMessage = message;
    }

    public ApiException(string message, ApiException innerException) : base(message, innerException)
    {
        Init();
        CustomMessage = message;
        TimeStamp = innerException.TimeStamp;
    }

    protected ApiException(SerializationInfo info, StreamingContext context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        CustomMessage = (string)info.GetValue(nameof(CustomMessage), typeof(string));
        TimeStamp = (DateTime)info.GetValue(nameof(TimeStamp), typeof(DateTime));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        info.AddValue(nameof(Message), Message, typeof(string));
        info.AddValue(nameof(CustomMessage), CustomMessage, typeof(string));
        info.AddValue(nameof(StackTrace), StackTrace, typeof(Exception));
        info.AddValue(nameof(InnerException), InnerException, typeof(Exception));
        info.AddValue(nameof(TimeStamp), TimeStamp, typeof(DateTime));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.AppendFormat(string.Format(@"Message: [{0}]. ", CustomMessage), Environment.NewLine);
        strBuilder.AppendFormat(string.Format(@"TimeStamp: [{0}]. ", TimeStamp), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected virtual void Init()
    {
        CustomMessage = string.Empty;
        TimeStamp = DateTime.Now;
    }

    public virtual Dictionary<string, object> CreateDictionary()
    {
        var dic = new Dictionary<string, object>
        {
            { nameof(Message), Message },
            { nameof(CustomMessage), CustomMessage },
            { nameof(TimeStamp), TimeStamp },
            { nameof(StackTrace), StackTrace }
        };
        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }
}