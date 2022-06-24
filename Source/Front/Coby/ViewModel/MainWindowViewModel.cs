using ApiModule.Api;
using ApiModule.Attributes;
using ApiModule.Operations;

namespace Coby.ViewModel;

[LicenceModule(5)]
internal class MainWindowViewModel : IIntegrationModule
{
    private readonly ModuleOperation _module;

    public MainWindowViewModel()
    {
        _module = ModuleOperation.GetInstance();
        _module.NotificationService.ReceiveOrder += NotificationService_ReceiveOrder;
        _module.NotificationService.ReceiveWaiter += NotificationService_ReceiveWaiter;

        /*var credentials = module.CredentialsOperation.CreateCredentials("ADMINPASSWORD");
        var waiters = module.WaiterOperation.GetWaiters();
        var waiter = waiters.First();

        module.WaiterOperation.OpenPersonalShift(credentials, waiter);

        //var q = module.TableOperation.CreateTable(credentials, 1, "Стол 1");
        var tables = module.TableOperation.GetTables();
        var table = tables.First();

        var order = module.OrderOperation.CreateOrder(credentials, waiter, table);*/
    }

    private void NotificationService_ReceiveWaiter(Shared.Data.IEntityChangedEvent<Shared.Data.IWaiter> obj)
    {
        //throw new NotImplementedException();
    }

    private void NotificationService_ReceiveOrder(Shared.Data.IEntityChangedEvent<Shared.Data.IOrder> obj)
    {
        //throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}