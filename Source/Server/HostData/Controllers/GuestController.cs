﻿using HostData.Cache.Orders;
using HostData.Controllers.LogFactory;
using HostData.Model;
using Microsoft.Extensions.Logging;

namespace HostData.Controllers;

internal class GuestController : LoggerController
{
    private readonly IOrderCache _orderCache;

    public GuestController(ILogger logger, IOrderCache orderCache) : base(logger)
    {
        _orderCache = orderCache;
    }

    public Session AddGuest(dynamic id, Session session)
    {
        if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
            throw new ArgumentException(nameof(id));

        if (session.Orders.All(x => x.Id.Equals(orderId)) is false)
            throw new ArgumentException($"Нельзя добавлять в одну сессию разные id {nameof(id)}");

        var order = _orderCache.GetOrderById(orderId);
        var guestsList = session.Orders.Count <= 0
            ? order.GetGuests()
            : session.Orders.OrderByDescending(x => x.Version).First().GetGuests();

        var guest = new Guest(Guid.NewGuid(), $"Guest {guestsList.Count + 1}", guestsList.Count + 1);
        guestsList.Add(guest);
        var newOrder = new Order(order.Id,
                                 order.TableId,
                                 order.WaiterId,
                                 order.StartTime,
                                 order.EndTime,
                                 order.OrderStatus,
                                 session.Version + 1,
                                 guestsList,
                                 order.IsDeleted);

        session.Orders.Add(newOrder);
        session.Version++;
        return session;
    }
}