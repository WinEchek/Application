using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Game;
using WinEchek.Model;
using WinEchek.ModelView;
using WinEchek.Network;
using WinEchek.Views.Windows;
using Color = WinEchek.Model.Pieces.Color;

namespace WinEchek.Views
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
                if (t.AddressFamily == AddressFamily.InterNetwork)
                {
                    ComboBoxIP.Items.Add(t.ToString());
                }
            }
            if (ComboBoxIP.Items.Count == 0)
            {
                _mainWindow.ShowMessageAsync("Erreur réseau",
                    "Vous n\'êtes connecté à aucun réseau, vous allez être redirigé vers l\'accueil.");
                _mainWindow.MainControl.Content = new Home(_mainWindow);
            }
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxIP.SelectedItem == null || TextBoxGameName.Text == "" || TextBoxPort.Text == "" ||
                TextBoxPseudo.Text == "")
            {
                _mainWindow.ShowMessageAsync("Paramètres incorrects", "Veuillez remplir tout les champs.");
                return;
            }
            Uri uri =
                new Uri("net.tcp://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxPseudo.Text);
            WaitJoinWindow waitJoinWindow = new WaitJoinWindow(uri, GetComboBoxColor());
            if (waitJoinWindow.ShowDialog() == true)
            {
                GameFactory gameFactory = new GameFactory();
                BoardView boardView = new BoardView(_container);
                Core.Game game = gameFactory.CreateGame(Mode.Network, _container, boardView, GetComboBoxColor(), null);
                _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
            }
            else
            {
                // Attention : pratique choquante
                try
                {
                    NetworkServiceHost.Close();
                }
                catch (Exception)
                {
                    ; //Rien à faire (technique de pro)
                }
                _mainWindow.ShowMessageAsync("Erreur réseau", "Il y a eu un problème lors de la connexion avec l'autre joueur... Vueillez réessayer.");
            }

        }

        private Color GetComboBoxColor()
        {
            return ComboBoxColor.Text == "Blanc" ? Color.White : Color.Black;
        }
    }
}