using Api.Operations;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
        var orders = ModuleOperation.OrderOperation.GetOrders();

        var removeOrder = ModuleOperation.OrderOperation.DeleteOrder(order);
        var orders2 = ModuleOperation.OrderOperation.GetOrders();
    }
}