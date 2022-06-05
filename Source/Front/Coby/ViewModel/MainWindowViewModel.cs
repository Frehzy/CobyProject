using Api.Operations;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
        var orders = ModuleOperation.OrderOperation.GetOrders();
        var session = ModuleOperation.OrderOperation.CreateSession(order.Id);
        ModuleOperation.GuestOperation.CreateGuest(order, ref session);
        ModuleOperation.GuestOperation.CreateGuest(order, ref session);
        ModuleOperation.OrderOperation.SubmitChanges(ref session);

        var orders2 = ModuleOperation.OrderOperation.GetOrders();
    }
}