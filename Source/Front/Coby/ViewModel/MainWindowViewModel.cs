using Api.Operations;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var waiters = ModuleOperation.WaiterOperation.GetWaiters().ToList();
        var credentials = ModuleOperation.CredentialsOperation.CreateCredentials("ADMINPASSWORD");
        var waiter = ModuleOperation.WaiterOperation.OpenPersonalShift(credentials, waiters.First());
        var tables2 = ModuleOperation.TableOperation.GetTables();
        var table = ModuleOperation.TableOperation.CreateTable(credentials, 1, "Стол 1");
        var tables = ModuleOperation.TableOperation.GetTables();

        var result = ModuleOperation.TableOperation.RemoveTable(credentials, tables.First());
    }
}