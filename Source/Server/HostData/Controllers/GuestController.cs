using HostData.Cache.Orders;
using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class GuestController : BaseController
{
    private readonly IOrderCache _orderCache;

    public GuestController(IOrderCache orderCache, IWaiterCache waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
    }

    public async Task<SessionDto> AddGuest(dynamic orderId, dynamic credentialsId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, oId, "Нельзя добавлять в одну сессию разные id");

            CheckCredentials(cId, EmployeePermission.CanAddGuestOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));

            var guestsList = session.Orders.Count <= 0
                ? order.GetGuests()
                : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

            var guest = new GuestDto(Guid.NewGuid(), $"Guest {guestsList.Count + 1}", guestsList.Count + 1, false);
            guestsList.Add(guest);
            var newOrder = order with { Guests = guestsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    internal async Task<SessionDto> RemoveGuest(dynamic orderId, dynamic credentialsId, dynamic guestId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var gId = CheckDynamicGuid(guestId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            CheckCredentials(cId, EmployeePermission.CanRemoveGuestOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var guestsList = session.Orders.Count <= 0
                ? order.GetGuests()
                : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

            var guest = guestsList.First(x => x.Id.Equals(gId));
            guest = guest with { IsDeleted = true };

            var newOrder = order with { Guests = guestsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }
}