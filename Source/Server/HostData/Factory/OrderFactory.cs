using Api.Data.Order;
using HostData.Model;

namespace HostData.Factory;

internal static class OrderFactory
{
    public static Order CreateOrder(IOrder order) =>
        new(order.OrderId, order.TableId, order.WaiterId, order.StartTime, order?.EndTime, order.OrderStatus, order.IsDeleted);
}