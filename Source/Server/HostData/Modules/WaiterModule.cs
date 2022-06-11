using HostData.Cache.Config;
using HostData.Cache.Waiters;
using HostData.Controllers;

namespace HostData.Modules;

public class WaiterModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly WaiterController _waiterController;

    public WaiterModule(IWaiterCache waiterCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _waiterController = new(waiterCache);

        Get("/waiters", parameters =>
        {
            return Execute(Context, () => _waiterController.GetWaiters());
        });
    }
}