using Api.Operations;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var list = ModuleOperation.WaiterOperation.GetWaiters().ToList();
    }
}