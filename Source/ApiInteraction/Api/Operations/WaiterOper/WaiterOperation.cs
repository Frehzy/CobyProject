using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.WaiterOper;

internal class WaiterOperation : IWaiterOperation
{
    public IReadOnlyList<IWaiter> GetWaiters()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "waiters");
        var result = HttpRequest.Get<List<WaiterDto>>(uri);
        return result.Content.Select(x => WaiterFactory.Create(x)).ToList();
    }
}