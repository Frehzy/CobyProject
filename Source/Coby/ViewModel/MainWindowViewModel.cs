using Api;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var result = Http.Get(1);
    }
}