using HostData.Cache.Waiters;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class WaiterController
{
    private readonly IWaiterCache _waiterCache;

    public WaiterController(IWaiterCache waiterCache)
    {
        _waiterCache = waiterCache;
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        return await Task.Run(() =>
        {
            return _waiterCache.Waiters.Select(x => WaiterFactory.CreateDto(x)).ToList();
        });
    }
}