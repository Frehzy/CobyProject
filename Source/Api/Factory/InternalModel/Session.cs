using Api.Data;
using Api.Data.Order;

namespace Api.Factory.InternalModel;

internal class Session : ISession
{
    public Guid OrderId { get; set; }

    public IReadOnlyList<IOrder> Orders { get; set; }

    public int Version { get; set; }

    public Session(Guid orderId, int version)
    {
        OrderId = orderId;
        Orders = new List<Order>();
        Version = version;
    }

    public Session(Guid orderId, List<Order> newOrders, int version)
    {
        OrderId = orderId;
        Orders = newOrders;
        Version = version;
    }
}
