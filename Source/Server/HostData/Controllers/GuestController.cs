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

    public async Task<SessionDto> AddGuest(dynamic id, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
                throw new ArgumentException($"{nameof(id)} must be type Guid", nameof(id));

            if (session.OrderId.Equals(orderId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            var order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));
            var guestsList = session.Orders.Count <= 0
                ? order.GetGuests()
                : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

            var guest = new GuestDto(Guid.NewGuid(), $"Guest {guestsList.Count + 1}", guestsList.Count + 1, false);
            guestsList.Add(guest);
            var newOrder = order with { Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }
}