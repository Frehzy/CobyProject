using Shared.Data;

namespace Api.Operations.OrderOper;

public interface IOrderOperation
{
    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, ITable table);

    public IOrder GetOrderById(Guid orderId);

    public IReadOnlyList<IOrder> GetOrders();

    public bool DeleteOrder(IOrder order, ICredentials credentials);

    public ICredentials CreateCredentials(IWaiter waiter);

    public ISession CreateSession(Guid orderId);

    public IOrder SubmitChanges(ICredentials credentials, ref ISession session);
}