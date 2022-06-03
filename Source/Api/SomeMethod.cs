using Api.Data.Order;
using Api.Http;
using Api.InternalModel;
using Api.Operations;

namespace Api;

public static class SomeMethod
{
    public static IOrder Get(Guid orderId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders", orderId);
        var result = HttpRequest.Get<Order>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString());
        return result.Content;
    }

    public static IEnumerable<IOrder> Get()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "orders");
        var result = HttpRequest.Get<List<Order>>(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString());
        return result.Content;
    }

    public static void Post(IOrder order)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "add/order");
        var result = HttpRequest.Post(uri, ModuleOperation.ConfigSettings.OrganizationId.ToString(), order);
    }
}