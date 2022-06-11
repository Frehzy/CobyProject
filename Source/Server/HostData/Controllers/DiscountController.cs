using HostData.Cache.Discounts;
using HostData.Cache.Orders;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class DiscountController
{
    private readonly IOrderCache _orderCache;
    private readonly IDiscountCache _discountCache;

    public DiscountController(IOrderCache orderCache, IDiscountCache discountCache)
    {
        _orderCache = orderCache;
        _discountCache = discountCache;
    }

    internal async Task<SessionDto> AddDiscount(dynamic orderId, dynamic discountId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (Guid.TryParse(discountId.ToString(), out Guid dId) is false)
                throw new ArgumentException($"{nameof(discountId)} must be type Guid", nameof(discountId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var discountDto = DiscountFactory.CreateDto(_discountCache.GetDiscountById(dId));

            var discountsList = session.Orders.Count <= 0
                ? order.GetDiscounts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

            discountsList.Add(discountDto);
            var newOrder = order with { Discounts = discountsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    internal async Task<SessionDto> RemoveDiscount(dynamic orderId, dynamic discountId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (Guid.TryParse(discountId.ToString(), out Guid dId) is false)
                throw new ArgumentException($"{nameof(discountId)} must be type Guid", nameof(discountId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var discountsList = session.Orders.Count <= 0
                ? order.GetDiscounts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

            var discount = discountsList.First(x => x.Id.Equals(dId));
            discount = discount with { IsDeleted = true };

            var newOrder = order with { Discounts = discountsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    internal async Task<List<DiscountDto>> GetDiscounts()
    {
        return await Task.Run(() =>
        {
            return _discountCache.Discounts.Select(x => DiscountFactory.CreateDto(x)).ToList();
        });
    }
}