namespace HostData.Controllers;

/*internal class DiscountController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<IDiscount> _discountCache;

    public DiscountController(IOrderCache orderCache, IBaseCache<IDiscount> discountCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _discountCache = discountCache;
    }

    public async Task<SessionDto> AddDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        var oId = (Guid)CheckDynamicGuid(orderId);
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var dId = (Guid)CheckDynamicGuid(discountId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanAddDiscountOnOrder).ConfigureAwait(false);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        var discountDto = DiscountFactory.CreateDto(_discountCache.GetById(dId));

        var discountsList = session.Orders.Count <= 0
            ? order.GetDiscounts()
            : session.Orders.OrderByDescending(x => x.Version).First().GetDiscounts();

        discountsList.Add(discountDto);
        var newOrder = order with { Discounts = discountsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return session with { Version = session.Version + 1 };
    }

    public async Task<SessionDto> RemoveDiscount(dynamic orderId, dynamic credentialsId, dynamic discountId, SessionDto session)
    {
        var oId = (Guid)CheckDynamicGuid(orderId);
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var dId = (Guid)CheckDynamicGuid(discountId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanRemoveDiscountOnOrder).ConfigureAwait(false);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

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
    }

    public async Task<List<DiscountDto>> GetDiscounts()
    {
        return _discountCache.Values.Select(x => DiscountFactory.CreateDto(x)).ToList();
    }
}*/