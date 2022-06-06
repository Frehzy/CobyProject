using HostData.Cache.Orders;
using HostData.Controllers.LogFactory;
using Microsoft.Extensions.Logging;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class GuestController : LoggerController
{
    private readonly IOrderCache _orderCache;

    public GuestController(ILogger logger, IOrderCache orderCache) : base(logger)
    {
        _orderCache = orderCache;
    }

    public SessionDto AddGuest(dynamic id, SessionDto session)
    {
        if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
            throw new ArgumentException(nameof(id));

        if (session.OrderId.Equals(orderId) is false)
            throw new ArgumentException($"Нельзя добавлять в одну сессию разные id {nameof(id)}");

        var order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));
        var guestsList = session.Orders.Count <= 0
            ? order.GetGuests()
            : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

        var guest = new GuestDto(Guid.NewGuid(), $"Guest {guestsList.Count + 1}", guestsList.Count + 1);
        guestsList.Add(guest);
        var newOrder = new OrderDto(order.Id,
                                 order.TableId,
                                 order.WaiterId,
                                 order.StartTime,
                                 order.EndTime,
                                 order.Status,
                                 session.Version + 1,
                                 guestsList,
                                 order.IsDeleted);

        session.Orders.Add(newOrder);
        return session with { Version = session.Version + 1 };
    }
}