using System.Text.Json.Serialization;

namespace Shared.Data.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductType : byte
{
    Goods, //продукт для блюда
    Dish, //блюдо
    Modifier //модификатор для блюда
}