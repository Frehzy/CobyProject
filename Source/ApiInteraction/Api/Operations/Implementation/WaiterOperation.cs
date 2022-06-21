using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class WaiterOperation : IWaiterOperation
{
    public IWaiter AddPermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission)
    {
        var result = Request<WaiterDto>($"{credentials.Id}/waiter/update/addPermission/{waiter.Id}/{permission}");
        return WaiterFactory.Create(result);
    }

    public IWaiter ClosePersonalShift(ICredentials credentials, IWaiter waiter)
    {
        var result = Request<WaiterDto>($"{credentials.Id}/waiter/personalShift/close/{waiter.Id}");
        return WaiterFactory.Create(result);
    }

    public IWaiter CreateWaiter(ICredentials credentials, string name, string password)
    {
        var result = Request<WaiterDto>($"{credentials.Id}/waiter/create/{name}/{password}");
        return WaiterFactory.Create(result);
    }

    public IWaiter GetWaiterById(Guid waiterId)
    {
        var result = Request<WaiterDto>($"waiter/{waiterId}");
        return WaiterFactory.Create(result);
    }

    public IReadOnlyList<IWaiter> GetWaiters()
    {
        var result = Request<List<WaiterDto>>($"waiters");
        return result.Select(x => WaiterFactory.Create(x)).ToList();
    }

    public IWaiter OpenPersonalShift(ICredentials credentials, IWaiter waiter)
    {
        var result = Request<WaiterDto>($"{credentials.Id}/waiter/personalShift/open/{waiter.Id}");
        return WaiterFactory.Create(result);
    }

    public IWaiter RemovePermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission)
    {
        var result = Request<WaiterDto>($"{credentials.Id}/waiter/update/removePermission/{waiter.Id}/{permission}");
        return WaiterFactory.Create(result);
    }

    public bool RemoveWaiter(ICredentials credentials, IWaiter waiter)
    {
        return Request<WaiterDto>($"{credentials.Id}/waiter/remove/{waiter.Id}") is not null;
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        return result.Content;
    }
}