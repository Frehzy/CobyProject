using Api.Http;
using Shared.Data;
using Shared.Factory;

namespace Api.Operations.GuestOper;

internal class GuestOperation : IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(IOrder order, ICredentials credentials, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/guest/add/{credentials.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetGuests()).ToList();
    }

    public IReadOnlyList<IGuest> RemoveGuest(IOrder order, ICredentials credentials, IGuest guest, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/guest/remove/{credentials.Id}/{guest.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetGuests()).ToList();
    }
}