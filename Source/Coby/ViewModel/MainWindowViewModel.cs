using Api.Operations;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
        var orders = ModuleOperation.OrderOperation.GetOrders();
        var session = ModuleOperation.OrderOperation.CreateSession(order.Id);
        ModuleOperation.GuestOperation.CreateGuest(orders.Last(), ref session);
        ModuleOperation.GuestOperation.CreateGuest(orders.Last(), ref session);
        ModuleOperation.OrderOperation.SubmitChanges(ref session);

        ModuleOperation.OrderOperation.GetOrders();
        var session2 = ModuleOperation.OrderOperation.CreateSession(order.Id);
        ModuleOperation.GuestOperation.CreateGuest(orders.Last(), ref session2);
        ModuleOperation.OrderOperation.SubmitChanges(ref session2);
    }
}