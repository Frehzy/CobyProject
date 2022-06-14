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
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post<SessionDto, OrderDto>(uri, sessionDto)).Result;
        session = default;
        return OrderFactory.Create(result.Content);
    }

    public ICredentials CreateCredentials(string waiterPassword)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"credentials/{waiterPassword}");
        var result = Task.Run(async () => await HttpRequest.Get<CredentialsDto>(uri)).Result;
        return CredentialsFactory.Create(result.Content);
    }

    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, IReadOnlyList<ITable> tables)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/create/{credentials.Id}/{waiter.Id}/{string.Join("/", tables.Select(x => x.Id))}");
        var result = Task.Run(async () => await HttpRequest.Get<OrderDto>(uri)).Result;
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
        var result = Task.Run(async () => await HttpRequest.Post<OrderDto, OrderDto>(uri, OrderFactory.CreateDto(order))).Result;
        return result.Content != null;
    }

    public IOrder GetOrderById(Guid orderId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"orders/{orderId}");
        var result = Task.Run(async () => await HttpRequest.Get<OrderDto>(uri)).Result;
        return OrderFactory.Create(result.Content);
    }

    public IReadOnlyList<IOrder> GetOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "allOrders");
        var result = Task.Run(async () => await HttpRequest.Get<List<OrderDto>>(uri)).Result;
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IReadOnlyList<IOrder> GetOpenOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "openOrders");
        var result = Task.Run(async () => await HttpRequest.Get<List<OrderDto>>(uri)).Result;
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IOrder SubmitChanges(ICredentials credentials, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"order/submitChanges/{credentials.Id}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post<SessionDto, OrderDto>(uri, sessionDto)).Result;
        session = default;
        return OrderFactory.Create(result.Content);
    }
}