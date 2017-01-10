using System;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Game;
using WinEchek.ModelView;
using WinEchek.Network;
using Color = WinEchek.Model.Pieces.Color;
using Container = WinEchek.Model.Container;
using GameFactory = WinEchek.Game.GameFactory;

namespace WinEchek.Views
{
    /// <summary>
    /// Logique d'interaction pour JoinGameOptions.xaml
    /// </summary>
    public partial class JoinGameOptions : UserControl
    {
        private MainWindow _mainWindow;
        private Container _container;

        public JoinGameOptions(MainWindow mainWindow, Container container)
        {
            InitializeComponent();
            _container = container;
            _mainWindow = mainWindow;
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

        private void ButtonJoin_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxIP.SelectedItem == null ||
                TextBoxGameName.Text == "" ||
                TextBoxPort.Text == "" ||
                TextBoxPseudo.Text == "" ||
                TextBoxHostIP.Text == "" ||
                TextBoxHostPort.Text == "" ||
                TextBoxHostPseudo.Text == "")
            {
                _mainWindow.ShowMessageAsync("Paramètres incorrects", "Veuillez remplir tout les champs");
                return;
            }


            //Création du service
            Uri uri =
                new Uri("net.tcp://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxPseudo.Text);

            NetworkServiceHost.Create(uri);
            NetworkServiceHost.Open();

            //On créer le client et on informe l'autre service de l'adresse de notre service
            Uri hostUri =
                new Uri("net.tcp://" + TextBoxHostIP.Text + ":" + TextBoxHostPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxHostPseudo.Text);
            EndpointAddress endpointAddress = new EndpointAddress(hostUri);
            NetworkServiceClient.Create(endpointAddress);
            try
            {
                NetworkServiceClient.Channel().SendClientAdress(uri.ToString());
            }
            catch (Exception)
            {
                _mainWindow.ShowMessageAsync("Erreur réseau",
                    "Il y a eu un problème lors de la connexion avec l\'autre joueur, cela peut provenir des paramètres que vous avez saisis.");
                return;
            }

            Color distantPlayerColor = NetworkServiceClient.Channel().GetColor() == "White" ? Color.White : Color.Black;
            Color color = distantPlayerColor == Color.White ? Color.Black : Color.White;
            NetworkServiceHost.GetService().PlayerColor = color;

            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container);
            Core.Game game = gameFactory.CreateGame(Mode.Network, _container, boardView, color, null);
            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);

        }
    }
}
