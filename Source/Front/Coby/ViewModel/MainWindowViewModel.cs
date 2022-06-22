using Api.Operations;
using Api.Operations.Contracts;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        IServiceProvider service = Api.Notification.Configure.ConfigureServices();
        var module = new ModuleOperation(service);
        module.NotificationService.OnOrder += NotificationService_OnOrder;


        var credentials = module.CredentialsOperation.CreateCredentials("ADMINPASSWORD");
        var waiters = module.WaiterOperation.GetWaiters();
        var waiter = waiters.First();

        module.WaiterOperation.OpenPersonalShift(credentials, waiter);

        //var q = module.TableOperation.CreateTable(credentials, 1, "Стол 1");
        var tables = module.TableOperation.GetTables();
        var table = tables.First();

        var order = module.OrderOperation.CreateOrder(credentials, waiter, table);
    }

    private void NotificationService_OnOrder(Shared.Data.IOrder order)
    {
        throw new NotImplementedException();
    }
}