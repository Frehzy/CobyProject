using Api;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        var result = Http.Get(2);

        Http.Post(result);
    }
}