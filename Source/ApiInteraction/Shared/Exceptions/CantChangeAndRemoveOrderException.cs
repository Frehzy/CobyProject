using Shared.Data;
using Shared.Factory.InternalModel;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

public class CantChangeAndRemoveOrderException : ViolationBusinessLogicException
{
    public Guid OrderId { get; set; }

    public override string Message => ToString();

    public CantChangeAndRemoveOrderException(Guid orderId) : base(nameof(CantChangeAndRemoveOrderException))
    {
        OrderId = orderId;
    }

    protected CantChangeAndRemoveOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        OrderId = (Guid)info.GetValue(nameof(OrderId), typeof(Guid));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        dic.Add(nameof(OrderId), OrderId);

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Order), Message, typeof(IOrder));
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());
        strBuilder.AppendFormat(string.Format(@"OrderId: [{0}]. ", OrderId), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        OrderId = default;
    }
}