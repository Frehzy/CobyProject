using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coby.ViewModel;

internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        Api.Http.Get(1);
    }
}