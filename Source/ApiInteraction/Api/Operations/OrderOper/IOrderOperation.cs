using Shared.Data;

namespace Api.Operations.OrderOper;

public interface IOrderOperation
{
    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, IReadOnlyList<ITable> tables);

    public IOrder GetOrderById(Guid orderId);

    public IReadOnlyList<IOrder> GetOrders();

    public IReadOnlyList<IOrder> GetOpenOrders();

    public bool DeleteOrder(IOrder order, ICredentials credentials);

    public ICredentials CreateCredentials(string waiterPassword);

    public ISession CreateSession(Guid orderId);

    public IOrder CloseOrder(ICredentials credentials, ref ISession session);

    public IOrder SubmitChanges(ICredentials credentials, ref ISession session);
}