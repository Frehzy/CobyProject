using Shared.Data;
using Shared.Factory.InternalModel;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

[Serializable]
public sealed class CantAddProductException : ViolationBusinessLogicException
{
    public IProduct Product { get; set; }

    public override string Message => ToString();

    public CantAddProductException(IProduct product) : base(nameof(CantAddProductException))
    {
        Product = product;
    }

    protected CantAddProductException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        Product = (Product)info.GetValue(nameof(Product), typeof(Product));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        foreach(var item in typeof(Product).GetProperties())
            dic.Add(nameof(item.Name), item.GetValue(Product));

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());

        var properties = typeof(Product).GetProperties();
        var result = properties.Select(x => $"{x.Name}: {x.GetValue(Product, null)}");
        strBuilder.AppendFormat(string.Join(Environment.NewLine, result), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        Product = default;
    }
}