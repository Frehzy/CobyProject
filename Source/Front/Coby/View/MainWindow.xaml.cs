using Coby.ViewModel;
using System.Windows;

namespace Coby.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = new MainWindowViewModel();
        }
    }
}
