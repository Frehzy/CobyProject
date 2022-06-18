using System.Text.Json.Serialization;

namespace Shared.Data.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentTypeKind
{
    Unknown,
    Cash,
    Card,
    WriteOff,
    Voucher
}