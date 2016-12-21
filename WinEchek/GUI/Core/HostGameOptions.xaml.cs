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
using WinEchek.Core.Network;
using WinEchek.GUI.Core.Windows;
using WinEchek.Model;

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
            Uri uri = new Uri("http://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text + TextBoxPseudo.Text);
            WaitJoinWindow waitJoinWindow = new WaitJoinWindow(uri);
            Console.WriteLine(waitJoinWindow.ShowDialog());
        }
    }
}
