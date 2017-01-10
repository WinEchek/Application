using System.Windows;
using System.Windows.Controls;
using WinEchek.Model;

namespace WinEchek.Views
{
    /// <summary>
    /// Logique d'interaction pour HostJoin.xaml
    /// </summary>
    public partial class HostJoin : UserControl
    {
        private MainWindow _mainWindow;
        private Container _container;

        public HostJoin(MainWindow mainWindow, Container container)
        {
            InitializeComponent();
            _container = container;

            _mainWindow = mainWindow;
        }

        private void TileHostGame_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new HostGameOptions(_mainWindow, _container);
        }

        private void TileJoinGame_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new JoinGameOptions(_mainWindow, _container);
        }
    }
}
