using HostData.Cache;
using HostData.Cache.Order;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class GuestController : BaseController
{
    private readonly IOrderCache _orderCache;

    public GuestController(IOrderCache orderCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
    }

    public Task<SessionDto> AddGuest(dynamic orderId, dynamic credentialsId, SessionDto session)
    {
        var oId = CheckDynamicGuid(orderId);
        var cId = CheckDynamicGuid(credentialsId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, oId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanAddGuestOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        var guestsList = session.Orders.Count <= 0
            ? order.GetGuests()
            : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

        var guest = new GuestDto(Guid.NewGuid(), $"Guest {guestsList.Count + 1}", guestsList.Count + 1, false);
        guestsList.Add(guest);
        var newOrder = order with { Guests = guestsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return Task.FromResult(session with { Version = session.Version + 1 });
    }

    public Task<SessionDto> RemoveGuest(dynamic orderId, dynamic credentialsId, dynamic guestId, SessionDto session)
    {
        var oId = CheckDynamicGuid(orderId);
        var cId = CheckDynamicGuid(credentialsId);
        var gId = CheckDynamicGuid(guestId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        CheckCredentials(cId, EmployeePermission.CanRemoveGuestOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(orderId));

        var guestsList = session.Orders.Count <= 0
            ? order.GetGuests()
            : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

        var guest = guestsList.First(x => x.Id.Equals(gId));
        if (guest.IsDeleted is true)
            throw new CantRemoveDeletedItemException(guest.Id);
        guest = guest with { IsDeleted = true };

        var newOrder = order with { Guests = guestsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return Task.FromResult(session with { Version = session.Version + 1 });
    }
}