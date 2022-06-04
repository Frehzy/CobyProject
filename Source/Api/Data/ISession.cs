using Api.Data.Order;

namespace Api.Data;

public interface ISession
{
    IReadOnlyList<IOrder> Orders { get; }

    int Version { get; }
}