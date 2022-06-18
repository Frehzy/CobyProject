using Shared.Data;
using Shared.Data.Enum;

namespace Api.Operations.WaiterOper;

public interface IWaiterOperation
{
    public IReadOnlyList<IWaiter> GetWaiters();

    public IWaiter GetWaiterById(Guid waiterId);

    public IWaiter CreateWaiter(ICredentials credentials, string name, string password);

    public IWaiter RemoveWaiter(ICredentials credentials, IWaiter waiter);

    public IWaiter AddPermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission);

    public IWaiter RemovePermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission);
}