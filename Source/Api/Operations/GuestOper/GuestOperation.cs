using Api.Data;
using Api.Data.Order;
using Api.Factory;
using Api.Http;

namespace Api.Operations.GuestOper;

internal class GuestOperation : IGuestOperation
{
    public ISession CreateGuest(IOrder order, ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/guest/add");
        var result = HttpRequest.Post(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString(), SessionFactory.CreateDto(session));
        return SessionFactory.Create(result.Content);
    }

    public bool RemoveGuest(IOrder order, Guid guestId, ISession session)
    {
        throw new NotImplementedException();
    }
}