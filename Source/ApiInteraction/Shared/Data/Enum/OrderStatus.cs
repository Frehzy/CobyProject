using System.Text.Json.Serialization;

namespace Shared.Data.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus : byte
{
    Open,
    Closed,
    Deleted
}