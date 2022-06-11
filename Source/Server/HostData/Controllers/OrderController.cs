using HostData.Cache.Orders;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace HostData.Controllers;

internal class OrderController
{
    private readonly IOrderCache _orderCache;

    public OrderController(IOrderCache orderCache)
    {
        _orderCache = orderCache;
    }

    public async Task<OrderDto> GetOrderById(dynamic id)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
                throw new ArgumentException($"{nameof(id)} must be type Guid", nameof(id));

            return OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));
        });
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        return await Task.Run(() =>
        {
            return _orderCache.Orders.Select(x => OrderFactory.CreateDto(x)).ToList();
        });
    }

    public async Task<OrderDto> CreateOrder(dynamic waiterId, dynamic tableId)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(waiterId.ToString(), out Guid wId) is false)
                throw new ArgumentException($"{nameof(waiterId)} must be type Guid", nameof(waiterId));

            if (Guid.TryParse(tableId.ToString(), out Guid tId) is false)
                throw new ArgumentException($"{nameof(tableId)} must be type Guid", nameof(tableId));

            var order = new Order(Guid.NewGuid(), tId, wId, DateTime.Now, null, OrderStatus.Open, 1);
            _orderCache.AddOrUpdate(order);
            return OrderFactory.CreateDto(order);
        });
    }

    public async Task<OrderDto> SubmitChanges(SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (session.Orders.Count <= 0)
                throw new InvalidSessionException(session.Version, session.OrderId);

            var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();
            _orderCache.AddOrUpdate(OrderFactory.Create(lastOrder), session.Orders.Count);
            return OrderFactory.CreateDto(_orderCache.GetOrderById(lastOrder.Id));
        });
    }

    public async Task<OrderDto> RemoveOrderById(dynamic id)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
                throw new ArgumentException($"{nameof(id)} must be type Guid", nameof(id));

            return OrderFactory.CreateDto(_orderCache.RemoveOrder(orderId));
        });
    }
}