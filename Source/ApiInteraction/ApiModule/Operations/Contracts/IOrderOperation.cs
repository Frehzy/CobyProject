using Shared.Data;

namespace ApiModule.Operations.Contracts;

public interface IOrderOperation
{
    public IReadOnlyList<IOrder> GetOrders();

    public IOrder GetOrderById(Guid orderId);

    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, ITable table);

    public bool RemoveOrder(ICredentials credentials, IOrder order);

    public IReadOnlyList<IOrder> GetOpenOrders();
}