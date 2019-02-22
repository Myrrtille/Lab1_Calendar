using Lab1.ViewModels;
using System.Windows;

namespace Lab1.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private MainViewModel _mainViewModel;

        public MainView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;
        }
    }
}
