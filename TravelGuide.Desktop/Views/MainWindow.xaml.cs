using System.Windows;
using TravelGuide.Desktop.ViewModels;

namespace TravelGuide.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}
