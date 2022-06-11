using Shared.Data.Enum;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exceptions;

[Serializable]
public sealed class PermissionDeniedException : ApiException
{
    public EmployeePermission Permission { get; set; }

    public override string Message => ToString();

    public PermissionDeniedException(EmployeePermission permission) : base(nameof(PermissionDeniedException))
    {
        Permission = permission;
    }

    public PermissionDeniedException(EmployeePermission permission, string message) 
        : base(message)
    {
        Permission = permission;
    }

    public PermissionDeniedException(EmployeePermission permission, string message, ApiException innerException)
        : base(message, innerException)
    {
        Permission = permission;
    }

    protected PermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info is null)
            throw new ArgumentNullException(nameof(info));

        Permission = (EmployeePermission)info.GetValue(nameof(Permission), typeof(EmployeePermission));
    }

    public override Dictionary<string, object> CreateDictionary()
    {
        var dic = base.CreateDictionary();
        dic.Add(nameof(EmployeePermission), Permission);

        foreach (DictionaryEntry data in Data)
            dic.Add(data.Key.ToString(), data.Value);

        return dic;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append(base.ToString());
        strBuilder.AppendFormat(string.Format(@"Permission: [{0}]. ", Permission), Environment.NewLine);
        return strBuilder.ToString();
    }

    protected override void Init()
    {
        base.Init();
        Permission = default;
    }
}