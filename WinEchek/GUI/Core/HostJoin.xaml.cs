using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinEchek.GUI.Core
{
    /// <summary>
    /// Logique d'interaction pour HostJoin.xaml
    /// </summary>
    public partial class HostJoin : UserControl
    {
        private MainWindow _mainWindow;
        public HostJoin(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void TileHostGame_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new HostGameOptions(_mainWindow);
        }

        private void TileJoinGame_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
