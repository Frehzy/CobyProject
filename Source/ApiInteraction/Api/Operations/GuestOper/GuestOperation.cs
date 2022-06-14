﻿using Api.Http;
using Shared.Data;
using Shared.Factory;

namespace Api.Operations.GuestOper;

internal class GuestOperation : IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(ICredentials credentials, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/guest/add/{credentials.Id}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetGuests()).ToList();
    }

    public IReadOnlyList<IGuest> RemoveGuest(ICredentials credentials, IGuest guest, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/guest/remove/{credentials.Id}/{guest.Id}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetGuests()).ToList();
    }
}