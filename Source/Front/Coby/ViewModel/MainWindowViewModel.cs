using Api.Operations;
using Shared.Exceptions;
using System.Windows;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        try
        {
            var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
            var session = ModuleOperation.OrderOperation.CreateSession(order.Id);
            ModuleOperation.OrderOperation.SubmitChanges(ref session);
        }
        catch (EntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (InvalidSessionException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        /*var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
        var orders = ModuleOperation.OrderOperation.GetOrders();
        var session = ModuleOperation.OrderOperation.CreateSession(order.Id);
        ModuleOperation.GuestOperation.CreateGuest(order, ref session);
        ModuleOperation.GuestOperation.CreateGuest(order, ref session);
        ModuleOperation.OrderOperation.SubmitChanges(ref session);

        var orders2 = ModuleOperation.OrderOperation.GetOrders();*/
    }
}