using System.Runtime.Serialization;

namespace Shared.Exceptions;

[Serializable]
public class InvalidLicenceModuleException : ApiException
{
    public override string Message => ToString();

    public InvalidLicenceModuleException() : base(nameof(InvalidLicenceModuleException)) { }

    public InvalidLicenceModuleException(string message) : base(message) { }

    public InvalidLicenceModuleException(string message, ApiException innerException) : base(message, innerException) { }

    protected InvalidLicenceModuleException(SerializationInfo info, StreamingContext context) : base(info, context)
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