using Shared.Data;

namespace Api.Operations.WaiterOper;

public interface IWaiterOperation
{
    public IReadOnlyList<IWaiter> GetWaiters();
}