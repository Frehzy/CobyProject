using Api.Data.Order;

namespace Api.Data;

public interface ISession
{
    Guid OrderId { get; set; }

    IReadOnlyList<IOrder> Orders { get; }

    int Version { get; }
}