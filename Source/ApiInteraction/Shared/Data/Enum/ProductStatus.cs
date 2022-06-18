using System.Text.Json.Serialization;

namespace Shared.Data.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductStatus : byte
{
    Added,
    Printed,
    Served,
    Deleted
}