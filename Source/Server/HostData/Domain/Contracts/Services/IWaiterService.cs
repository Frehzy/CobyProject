using HostData.Domain.Contracts.Models;

namespace HostData.Domain.Contracts.Services;

public interface IWaiterService : IBaseService<WaiterModel>
{
    public IWaiterPermissionService WaiterPermissionService { get; }

    public IPermissionService PermissionService { get; }
}