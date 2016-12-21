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

namespace WinEchek.GUI.Core.Windows
{
    /// <summary>
    /// Logique d'interaction pour WaitJoinWindow.xaml
    /// </summary>
    public partial class WaitJoinWindow
    {
        public NetworkGameServiceClient NetworkGameServiceClient { get; set; }
        public NetworkGameServiceHost NetworkGameServiceHost { get; set; }

        public WaitJoinWindow(Uri uri)
        {
            InitializeComponent();
            LabelWait.Content = "Création de la partie";
            NetworkGameServiceHost = new NetworkGameServiceHost(uri);
            NetworkGameServiceHost.Open();
            NetworkGameServiceHost.NetworkGameService.ClientUriReceived += NetworkGameServiceOnClientUriReceived;
            LabelWait.Content = "Attente d'un autre joueur";
        }

        private void NetworkGameServiceOnClientUriReceived(Uri uri)
        {
            LabelWait.Content = "Configuration de la partie";

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress(uri);

            try
            {
                NetworkGameServiceClient = new NetworkGameServiceClient(basicHttpBinding, endpointAddress);
                NetworkGameServiceClient.Open();
            }
            catch (Exception)
            {
                DialogResult = false;
            }
            if (NetworkGameServiceClient.State != CommunicationState.Opened)
            {
                DialogResult = false;
            }

            LabelWait.Content = "Tentative de connexion avec le client";
            DialogResult = Ping();
        }

        private bool Ping()
        {
            string testMessage = "42";
            try
            {
                NetworkGameServiceClient.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(4);
                string received = NetworkGameServiceClient.Echo(testMessage);
                if (received != testMessage)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
