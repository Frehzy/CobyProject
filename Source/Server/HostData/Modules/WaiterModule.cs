using HostData.Cache;
using HostData.Controllers;
using Shared.Data;

namespace HostData.Modules;

public class WaiterModule : BaseModule
{
    private readonly IConfigSettings _configCache;
    private readonly WaiterController _waiterController;

    public WaiterModule(IBaseCache<IWaiter> waiterCache, IConfigSettings configCache) : base()
    {
        _configCache = configCache;
        _waiterController = new(waiterCache);

        Get("/waiters", parameters =>
        {
            return Execute(Context, () => _waiterController.GetWaiters());
        });
    }
}