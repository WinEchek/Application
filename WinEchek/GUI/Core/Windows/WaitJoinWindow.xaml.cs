using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinEchek.Core.Network;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.GUI.Core.Windows
{
    /// <summary>
    /// Logique d'interaction pour WaitJoinWindow.xaml
    /// </summary>
    public partial class WaitJoinWindow
    { 
        public NetworkServiceHost NetworkGameServiceHost { get; set; }

        public WaitJoinWindow(Uri uri, Color color)
        {
            InitializeComponent();
            LabelWait.Content = "Création de la partie";
            NetworkGameServiceHost = new NetworkServiceHost(uri) {NetworkGameService = {PlayerColor = color}};
            NetworkGameServiceHost.Open();
            NetworkGameServiceHost.NetworkGameService.ClientUriReceived += NetworkGameServiceOnClientUriReceived;
            LabelWait.Content = "Attente d'un autre joueur";
        }

        private void NetworkGameServiceOnClientUriReceived(string uri)
        {
            LabelWait.Content = "Configuration de la partie";
            EndpointAddress endpointAddress = new EndpointAddress(uri);

            try
            {
                NetworkServiceClient.Create(endpointAddress);
            }
            catch (Exception)
            {
                NetworkGameServiceHost.Close();
                DialogResult = false;
            }

            LabelWait.Content = "Tentative de connexion avec le client";
            DialogResult = true;
        }

        private bool Ping()
        {
            string testMessage = "42";
            NetworkServiceClient.Channel().Echo(testMessage);
            try
            {
                string received = NetworkServiceClient.Channel().Echo(testMessage);
                if (received != testMessage)
                {
                    NetworkGameServiceHost.Close();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                NetworkGameServiceHost.Close();
                return false;
            }
        }
    }
}
