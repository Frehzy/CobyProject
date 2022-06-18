using Api.Http;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.WaiterOper;

internal class WaiterOperation : IWaiterOperation
{
    public IWaiter CreateWaiter(ICredentials credentials, string name, string password)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/waiter/create/{name}/{password}");
        var result = Task.Run(async () => await HttpRequest.Get<WaiterDto>(uri)).Result;
        return WaiterFactory.Create(result.Content);
    }

    public IWaiter GetWaiterById(Guid waiterId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"waiter/{waiterId}");
        var result = Task.Run(async () => await HttpRequest.Get<WaiterDto>(uri)).Result;
        return WaiterFactory.Create(result.Content);
    }

    public IReadOnlyList<IWaiter> GetWaiters()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "waiters");
        var result = Task.Run(async () => await HttpRequest.Get<List<WaiterDto>>(uri)).Result;
        return result.Content.Select(x => WaiterFactory.Create(x)).ToList();
    }

    public IWaiter RemoveWaiter(ICredentials credentials, IWaiter waiter)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/waiter/remove/{waiter.Id}");
        var result = Task.Run(async () => await HttpRequest.Get<WaiterDto>(uri)).Result;
        return WaiterFactory.Create(result.Content);
    }

    public IWaiter AddPermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/waiter/update/addPermission/{waiter.Id}/{permission}");
        var result = Task.Run(async () => await HttpRequest.Get<WaiterDto>(uri)).Result;
        return WaiterFactory.Create(result.Content);
    }

    public IWaiter RemovePermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/waiter/update/removePermission/{waiter.Id}/{permission}");
        var result = Task.Run(async () => await HttpRequest.Get<WaiterDto>(uri)).Result;
        return WaiterFactory.Create(result.Content);
    }
}