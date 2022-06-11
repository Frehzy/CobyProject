using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Api.Operations.OrderOper;

internal class OrderOperation : IOrderOperation
{
    public IOrder CreateOrder(IWaiter waiter, ITable table)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "order/create", waiter.Id, table.Id);
        var result = HttpRequest.Get<OrderDto>(uri);
        return OrderFactory.Create(result.Content);
    }

    public ISession CreateSession(Guid orderId)
    {
        var order = GetOrderById(orderId);
        return new Session(order.Id, order.Version);
    }

    public bool DeleteOrder(IOrder order)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "order/remove");
        var result = HttpRequest.Post<OrderDto, OrderDto>(uri, OrderFactory.CreateDto(order));
        return result.Content != null;
    }

    public IOrder GetOrderById(Guid orderId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders", orderId);
        var result = HttpRequest.Get<OrderDto>(uri);
        return OrderFactory.Create(result.Content);
    }

    public IReadOnlyList<IOrder> GetOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders");
        var result = HttpRequest.Get<List<OrderDto>>(uri);
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IOrder SubmitChanges(ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "order/submitChanges");
        var result = HttpRequest.Post<SessionDto, OrderDto>(uri, SessionFactory.CreateDto(session));
        session = default;
        return OrderFactory.Create(result.Content);
    }
}