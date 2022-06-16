using HostData.Controller.Contract;

namespace HostData.Modules;

public class WaiterModule : BaseModule
{
    private readonly IWaiterController _waiterController;

    public WaiterModule(IWaiterController waiterController) : base()
    {
        _waiterController = waiterController;

        //_waiterController.CreateWaiter("TestWaiter", "0001");

        Get("/waiters", parameters =>
        {
            return Execute(Context, () => _waiterController.GetWaiters());
        });
    }
}