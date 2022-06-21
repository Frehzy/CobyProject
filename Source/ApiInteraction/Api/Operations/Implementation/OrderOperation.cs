using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class OrderOperation : IOrderOperation
{
    public IOrder CreateOrder(ICredentials credentials, IWaiter waiter, ITable table)
    {
        var result = HttpRequest.Request<OrderDto>($"{credentials.Id}/order/create/{waiter.Id}/{table.Id}");
        return OrderFactory.Create(result);
    }

    public IReadOnlyList<IOrder> GetOpenOrders()
    {
        var result = HttpRequest.Request<List<OrderDto>>($"openOrders");
        return result.Select(x => OrderFactory.Create(x)).ToList();
    }

    public IOrder GetOrderById(Guid orderId)
    {
        var result = HttpRequest.Request<OrderDto>($"order/{orderId}");
        return OrderFactory.Create(result);
    }

    public IReadOnlyList<IOrder> GetOrders()
    {
        var result = HttpRequest.Request<List<OrderDto>>($"orders");
        return result.Select(x => OrderFactory.Create(x)).ToList();
    }

    public bool RemoveOrder(ICredentials credentials, IOrder order)
    {
        return HttpRequest.Request<OrderDto>($"{credentials.Id}/order/remove/{order.Id}") is not null;
    }
}