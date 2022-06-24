using Shared.Data;
using Shared.Data.Enum;

namespace ApiModule.Operations.Contracts;

public interface IWaiterOperation
{
    public IReadOnlyList<IWaiter> GetWaiters();

    public IWaiter GetWaiterById(Guid waiterId);

    public IWaiter CreateWaiter(ICredentials credentials, string name, string password);

    public bool RemoveWaiter(ICredentials credentials, IWaiter waiter);

    public IWaiter AddPermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission);

    public IWaiter RemovePermissionOnWaiter(ICredentials credentials, IWaiter waiter, EmployeePermission permission);

    public IWaiter OpenPersonalShift(ICredentials credentials, IWaiter waiter);

    public IWaiter ClosePersonalShift(ICredentials credentials, IWaiter waiter);
}