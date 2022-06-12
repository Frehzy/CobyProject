using HostData.Cache.Discounts;
using HostData.Cache.Orders;
using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class DiscountController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IDiscountCache _discountCache;

    public DiscountController(IOrderCache orderCache, IDiscountCache discountCache, IWaiterCache waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _discountCache = discountCache;
    }

    internal async Task<SessionDto> AddDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var dId = CheckDynamicGuid(discountId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            CheckCredentials(cId, EmployeePermission.CanAddDiscountOnOrder);

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

    internal async Task<SessionDto> RemoveDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var dId = CheckDynamicGuid(discountId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            CheckCredentials(cId, EmployeePermission.CanRemoveDiscountOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var discountsList = session.Orders.Count <= 0
                ? order.GetDiscounts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

            var discount = discountsList.First(x => x.Id.Equals(dId));
            if (discount.IsDeleted is true)
                throw new CantRemoveDeletedItemException(discount.Id);
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