using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.DiscountOper;

internal class DiscountOperation : IDiscountOperation
{
    public IReadOnlyList<IDiscount> AddDiscount(ICredentials credentials, IDiscount discount, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/discount/add/{credentials.Id}/{discount.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetDiscounts()).ToList();
    }

    public IReadOnlyList<IDiscount> GetDiscount()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "discounts");
        var result = HttpRequest.Get<List<DiscountDto>>(uri);
        return result.Content.Select(x => DiscountFactory.Create(x)).ToList();
    }

    public IReadOnlyList<IDiscount> RemoveDiscount(ICredentials credentials, IDiscount discount, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/discount/remove/{credentials.Id}/{discount.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetDiscounts()).ToList();
    }
}