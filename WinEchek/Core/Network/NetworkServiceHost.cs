using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WinEchek.Core.Network
{
    public class NetworkServiceHost
    {
        private ServiceHost _host;
        public NetworkService NetworkGameService { get; set; }

        public NetworkServiceHost(Uri uri)
        {
            NetworkGameService = new NetworkService();
            _host = new ServiceHost(NetworkGameService, uri);
            NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None);
            _host.AddServiceEndpoint(typeof(INetworkService), netTcpBinding, uri);
        }

        public void Open()
        {
            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            _host.Open();
        }

        public void Close()
        {
            _host.Close();
        }
    }
}