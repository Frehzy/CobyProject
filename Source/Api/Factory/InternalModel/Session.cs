using Api.Data;
using Api.Data.Order;

namespace Api.Factory.InternalModel;

internal class Session : ISession
{
    public IReadOnlyList<IOrder> Orders { get; set; }

    public int Version { get; set; }

    public Session()
    {
        Orders = new List<Order>();
        Version = 1;
    }

    public Session(List<Order> newOrders, int version)
    {
        Orders = newOrders;
        Version = version;
    }
}
