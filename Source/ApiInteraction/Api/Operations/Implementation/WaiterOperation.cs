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
        var result = HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/update/addPermission/{waiter.Id}/{permission}");
        return WaiterFactory.Create(result);
    }

    public IWaiter ClosePersonalShift(ICredentials credentials, IWaiter waiter)
    {
        var result = HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/personalShift/close/{waiter.Id}");
        return WaiterFactory.Create(result);
    }

    public IWaiter CreateWaiter(ICredentials credentials, string name, string password)
    {
        var result = HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/create/{name}/{password}");
        return WaiterFactory.Create(result);
    }

    public IWaiter GetWaiterById(Guid waiterId)
    {
        var result = HttpRequest.Request<WaiterDto>($"waiter/{waiterId}");
        return WaiterFactory.Create(result);
    }

    public IReadOnlyList<IWaiter> GetWaiters()
    {
        var result = HttpRequest.Request<List<WaiterDto>>($"waiters");
        return result.Select(x => WaiterFactory.Create(x)).ToList();
    }

    public IWaiter OpenPersonalShift(ICredentials credentials, IWaiter waiter)
    {
        var result = HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/personalShift/open/{waiter.Id}");
        return WaiterFactory.Create(result);
    }

    public IWaiter RemovePermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission)
    {
        var result = HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/update/removePermission/{waiter.Id}/{permission}");
        return WaiterFactory.Create(result);
    }

    public bool RemoveWaiter(ICredentials credentials, IWaiter waiter)
    {
        return HttpRequest.Request<WaiterDto>($"{credentials.Id}/waiter/remove/{waiter.Id}") is not null;
    }
}