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
            ModuleOperation.OrderOperation.GetOrderById(Guid.NewGuid());
        }
        catch (EntityNotFoundException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }

        try
        {
            var order = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
            var session = ModuleOperation.OrderOperation.CreateSession(order.Id);
            ModuleOperation.OrderOperation.SubmitChanges(ref session);
        }
        catch (EntityNotFoundException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        catch (InvalidSessionException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }

        var newOrder = ModuleOperation.OrderOperation.CreateOrder(Guid.NewGuid(), Guid.NewGuid());
        var orders = ModuleOperation.OrderOperation.GetOrders();
        var session2 = ModuleOperation.OrderOperation.CreateSession(newOrder.Id);
        ModuleOperation.GuestOperation.CreateGuest(newOrder, ref session2);
        ModuleOperation.GuestOperation.CreateGuest(newOrder, ref session2);
        ModuleOperation.OrderOperation.SubmitChanges(ref session2);

        var newOrders = ModuleOperation.OrderOperation.GetOrders();

        ModuleOperation.OrderOperation.DeleteOrder(newOrders.First());

        var newOrders2 = ModuleOperation.OrderOperation.GetOrders();

        ModuleOperation.OrderOperation.DeleteOrder(newOrders.First());

        var newOrders3 = ModuleOperation.OrderOperation.GetOrders();
    }
}