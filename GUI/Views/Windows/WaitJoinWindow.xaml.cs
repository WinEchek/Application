using System;
using System.ServiceModel;
using WinEchek.Network;
using Color = WinEchek.Model.Pieces.Color;

namespace WinEchek.Views.Windows
{
    /// <summary>
    /// Logique d'interaction pour WaitJoinWindow.xaml
    /// </summary>
    /// <summary>
    /// Logique d'interaction pour WaitJoinWindow.xaml
    /// </summary>
    public partial class WaitJoinWindow
    {
        public WaitJoinWindow(Uri uri, Color color)
        {
            InitializeComponent();
            LabelWait.Content = "Création de la partie";

            NetworkServiceHost.Create(uri);
            NetworkServiceHost.GetService().PlayerColor = color;
            NetworkServiceHost.Open();

            NetworkServiceHost.GetService().ClientUriReceived += NetworkGameServiceOnClientUriReceived;
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
                NetworkServiceHost.Close();
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
                    NetworkServiceHost.Close();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                NetworkServiceHost.Close();
                return false;
            }
        }
    }
}
