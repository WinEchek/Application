using System;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using WinEchek.Network;

namespace WinEchek.Core
{
    /// <summary>
    ///     Logique d'interaction pour JoinGameOptions.xaml
    /// </summary>
    public partial class JoinGameOptions : UserControl
    {
        private MainWindow _mainWindow;

        public JoinGameOptions(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            InitComboBoxIp();
        }

        public NetworkGameServiceClient NetworkGameServiceClient { get; set; }
        public NetworkGameServiceHost NetworkGameServiceHost { get; set; }

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
                ComboBoxIP.Items.Add(t.ToString());
        }

        private void ButtonJoin_OnClick(object sender, RoutedEventArgs e)
        {
            //Création du service
            Uri uri =
                new Uri("http://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxPseudo.Text);
            NetworkGameServiceHost = new NetworkGameServiceHost(uri);
            NetworkGameServiceHost.Open();

            //On créer le client et on informe l'autre service de l'adresse de notre service
            Uri hostUri =
                new Uri("http://" + TextBoxHostIP.Text + ":" + TextBoxHostPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxHostPseudo.Text);
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress(hostUri);

            NetworkGameServiceClient = new NetworkGameServiceClient(basicHttpBinding, endpointAddress);
            NetworkGameServiceClient.Open();
            NetworkGameServiceClient.SendClientAdress(uri);
        }
    }
}