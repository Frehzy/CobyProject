using HostData.Cache;
using HostData.Cache.Order;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class DiscountController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<IDiscount> _discountCache;

    public DiscountController(IOrderCache orderCache, IBaseCache<IDiscount> discountCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _discountCache = discountCache;
    }

    public Task<SessionDto> AddDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        var oId = CheckDynamicGuid(orderId);
        var cId = CheckDynamicGuid(credentialsId);
        var dId = CheckDynamicGuid(discountId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanAddDiscountOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(orderId));

        var discountDto = DiscountFactory.CreateDto(_discountCache.GetById(dId));

        var discountsList = session.Orders.Count <= 0
            ? order.GetDiscounts()
            : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

        discountsList.Add(discountDto);
        var newOrder = order with { Discounts = discountsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return Task.FromResult(session with { Version = session.Version + 1 });
    }

    public Task<SessionDto> RemoveDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        var oId = CheckDynamicGuid(orderId);
        var cId = CheckDynamicGuid(credentialsId);
        var dId = CheckDynamicGuid(discountId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanRemoveDiscountOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(orderId));

        var discountsList = session.Orders.Count <= 0
            ? order.GetDiscounts()
            : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

        var discount = discountsList.First(x => x.Id.Equals(dId));
        if (discount.IsDeleted is true)
            throw new CantRemoveDeletedItemException(discount.Id);
        discount = discount with { IsDeleted = true };

        var newOrder = order with { Discounts = discountsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return Task.FromResult(session with { Version = session.Version + 1 });
    }

    public Task<List<DiscountDto>> GetDiscounts()
    {
        return Task.FromResult(_discountCache.Values.Select(x => DiscountFactory.CreateDto(x)).ToList());
    }
}