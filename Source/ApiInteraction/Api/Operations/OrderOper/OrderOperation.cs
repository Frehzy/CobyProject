using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Api.Operations.OrderOper;

internal class OrderOperation : IOrderOperation
{
    public IOrder CloseOrder(ICredentials credentials, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/closeOrder/{credentials.Id}");
        var result = HttpRequest.Post<SessionDto, OrderDto>(uri, SessionFactory.CreateDto(session));
        session = default;
        return OrderFactory.Create(result.Content);
    }

    public ICredentials CreateCredentials(IWaiter waiter) => new Credentials(waiter.Id);

    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, IReadOnlyList<ITable> tables)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/create/{credentials.Id}/{waiter.Id}/{string.Join("/", tables.Select(x => x.Id))}");
        var result = HttpRequest.Get<OrderDto>(uri);
        return OrderFactory.Create(result.Content);
    }

    public ISession CreateSession(Guid orderId)
    {
        var order = GetOrderById(orderId);
        return new Session(order.Id, order.Version);
    }

    public bool DeleteOrder(IOrder order, ICredentials credentials)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/remove/{credentials.Id}");
        var result = HttpRequest.Post<OrderDto, OrderDto>(uri, OrderFactory.CreateDto(order));
        return result.Content != null;
    }

    public IOrder GetOrderById(Guid orderId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"orders/{orderId}");
        var result = HttpRequest.Get<OrderDto>(uri);
        return OrderFactory.Create(result.Content);
    }

    public IReadOnlyList<IOrder> GetOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "allOrders");
        var result = HttpRequest.Get<List<OrderDto>>(uri);
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IReadOnlyList<IOrder> GetOpenOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "openOrders");
        var result = HttpRequest.Get<List<OrderDto>>(uri);
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IOrder SubmitChanges(ICredentials credentials, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/submitChanges/{credentials.Id}");
        var result = HttpRequest.Post<SessionDto, OrderDto>(uri, SessionFactory.CreateDto(session));
        session = default;
        return OrderFactory.Create(result.Content);
    }
}