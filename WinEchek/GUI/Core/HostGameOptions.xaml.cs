using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Core;
using WinEchek.Core.Network;
using WinEchek.GUI.Core.Windows;
using WinEchek.Model;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.GUI.Core
{
    /// <summary>
    /// Logique d'interaction pour HostGameOptions.xaml
    /// </summary>
    public partial class HostGameOptions : UserControl
    {
        private MainWindow _mainWindow;
        private Container _container;

        public HostGameOptions(MainWindow mainWindow, Container container)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _container = container;

            InitComboBoxIp();
        }

        private void InitComboBoxIp()
        {
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            var strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            foreach (IPAddress t in addr)
            {
                ComboBoxIP.Items.Add(t.ToString());
            }
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("net.tcp://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text + TextBoxPseudo.Text);
            WaitJoinWindow waitJoinWindow = new WaitJoinWindow(uri, GetComboBoxColor());
            if (waitJoinWindow.ShowDialog() == true)
            {
                GameFactory gameFactory = new GameFactory();
                BoardView boardView = new BoardView(_container.Board);
                Game game = gameFactory.CreateNetworkGame(_container, boardView, waitJoinWindow.NetworkGameServiceHost, GetComboBoxColor());
                _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);

            }
            else
            {
                _mainWindow.ShowMessageAsync("Erreur réseau",
                    "Il y a eu un problème lors de la connexion avec l'autre joueur... Vueillez réessayer.",
                    MessageDialogStyle.Affirmative);
            }

        }

        private Color GetComboBoxColor()
        {
            return ComboBoxColor.Text == "Blanc" ? Color.White : Color.Black;
        }
    }
}
