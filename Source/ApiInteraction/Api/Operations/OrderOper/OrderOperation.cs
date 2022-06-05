using Api.Data;
using Api.Data.Order;
using Api.Factory;
using Api.Factory.Dto;
using Api.Factory.InternalModel;
using Api.Http;

namespace Api.Operations.OrderOper;

internal class OrderOperation : IOrderOperation
{
    public IOrder CreateOrder(Guid waiterId, Guid tableId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "order/create", waiterId, tableId);
        var result = HttpRequest.Get<OrderDto>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString());
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
        var result = HttpRequest.Post<OrderDto, OrderDto>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString(), OrderFactory.CreateDto(order));
        return result.Content != null;
    }

    public IOrder GetOrderById(Guid orderId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders", orderId);
        var result = HttpRequest.Get<OrderDto>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString());
        return OrderFactory.Create(result.Content);
    }

    public IReadOnlyList<IOrder> GetOrders()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders");
        var result = HttpRequest.Get<List<OrderDto>>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString());
        return result.Content.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IOrder SubmitChanges(ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "order/submitChanges");
        var result = HttpRequest.Post<SessionDto, OrderDto>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString(), SessionFactory.CreateDto(session));
        session = default;
        return OrderFactory.Create(result.Content);
    }
}