﻿using Api.Http;
using Shared.Data;
using Shared.Factory;

namespace Api.Operations.GuestOper;

internal class GuestOperation : IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(IOrder order, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/guest/add");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.Guests).ToList();
    }

    public bool RemoveGuest(IOrder order, Guid guestId, ref ISession session)
    {
        throw new NotImplementedException();
    }
}