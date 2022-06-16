namespace HostData.Controllers;

/*internal class WaiterController : BaseController
{
    private readonly IBaseCache<IWaiter> _waiterCache;

    public WaiterController(IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _waiterCache = waiterCache;
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        return _waiterCache.Values.Select(x => WaiterFactory.CreateDto(x)).ToList();
    }
}*/