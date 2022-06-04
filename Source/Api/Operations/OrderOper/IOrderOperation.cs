using Api.Data;
using Api.Data.Order;

namespace Api.Operations.OrderOper;

public interface IOrderOperation
{
    public IOrder CreateOrder(Guid waiterId, Guid tableId);

    public IOrder GetOrderById(Guid orderId);

    public IReadOnlyList<IOrder> GetOrders();

    public bool DeleteOrder(IOrder order);

    public ISession CreateSession(Guid orderId);

    public IOrder SubmitChanges(ref ISession session);
}