using System.Text.Json.Serialization;

namespace Shared.Data.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentStatus
{
    New, //добавлен
    Finished, //обработан
    Returned, //возвращена
    Removed //удалён
}