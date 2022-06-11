using HostData.Cache.Orders;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class GuestController
{
    private readonly IOrderCache _orderCache;

    public GuestController(IOrderCache orderCache)
    {
        _orderCache = orderCache;
    }

    public async Task<SessionDto> AddGuest(dynamic orderId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, oId, "Нельзя добавлять в одну сессию разные id");

            var order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));
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

    internal async Task<SessionDto> RemoveGuestOnOrderById(dynamic orderId, dynamic guestId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (Guid.TryParse(guestId.ToString(), out Guid gId) is false)
                throw new ArgumentException($"{nameof(guestId)} must be type Guid", nameof(guestId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

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