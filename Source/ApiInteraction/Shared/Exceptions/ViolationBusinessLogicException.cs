using System.Runtime.Serialization;

namespace Shared.Exceptions;

[Serializable]
public abstract class ViolationBusinessLogicException : ApiException
{
    public override string Message => ToString();

    public ViolationBusinessLogicException() : base(nameof(ViolationBusinessLogicException)) { }

    public ViolationBusinessLogicException(string message)
        : base(message) { }

    public ViolationBusinessLogicException(string message, ApiException innerException)
        : base(message, innerException) { }

    protected ViolationBusinessLogicException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    public override Dictionary<string, object> CreateDictionary() =>
        base.CreateDictionary();

    public override void GetObjectData(SerializationInfo info, StreamingContext context) =>
        base.GetObjectData(info, context);

    public override string ToString() =>
        base.ToString();

    protected override void Init() =>
        base.Init();
}