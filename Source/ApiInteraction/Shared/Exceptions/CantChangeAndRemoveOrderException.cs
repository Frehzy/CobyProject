using Shared.Data;
using Shared.Factory.InternalModel;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

public class CantChangeAndRemoveOrderException : ViolationBusinessLogicException
{
    public IOrder Order { get; set; }

    public override string Message => ToString();

    public CantChangeAndRemoveOrderException(IOrder order) : base(nameof(CantChangeAndRemoveOrderException))
    {
        Order = order;
    }

    protected CantChangeAndRemoveOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        Order = (Order)info.GetValue(nameof(Order), typeof(Order));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        foreach (var item in typeof(Product).GetProperties())
            dic.Add(nameof(item.Name), item.GetValue(Order));

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

        var properties = typeof(Product).GetProperties();
        var result = properties.Select(x => $"{x.Name}: {x.GetValue(Order, null)}");
        strBuilder.AppendFormat(string.Join(Environment.NewLine, result), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        Order = default;
    }
}