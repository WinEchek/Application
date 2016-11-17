using System.Windows;
using System.Windows.Controls;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private MainWindow _mainWindow;
        public Home(MainWindow mw) {
            InitializeComponent();
            _mainWindow = mw;
        }

        private void LocalGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new CreateContinue(_mainWindow);
        }

        private void ContributeButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WinEchek/Application");
        }
    }
}
