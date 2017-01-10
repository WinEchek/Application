using System;
using System.ServiceModel;

namespace WinEchek.Network
{
    public class NetworkServiceHost
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private static NetworkServiceHost _networkServiceHost;
        /// <summary>
        /// Vrai si le service à été créer, ainsi que l'hôte
        /// </summary>
        private static bool _isCreated;

        /// <summary>
        /// Hôte du service
        /// </summary>
        private ServiceHost _host;
        /// <summary>
        /// Service hébérgé par l'hôte
        /// </summary>
        public NetworkService NetworkGameService { get; set; }

        private NetworkServiceHost(Uri uri)
        {
            NetworkGameService = new NetworkService();
            _host = new ServiceHost(NetworkGameService, uri);
            NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None);
            _host.AddServiceEndpoint(typeof(INetworkService), netTcpBinding, uri);
        }

        /// <summary>
        /// Permet de créer l'hôte de service
        /// </summary>
        /// <param name="uri"></param>
        public static void Create(Uri uri)
        {
            _networkServiceHost?._host.Close();
            _networkServiceHost = new NetworkServiceHost(uri);
            _isCreated = true;
        }

        /// <summary>
        /// Permet de récupérer le service hébérgé par l'hôte de service qui doit déjà être créée
        /// </summary>
        /// <returns></returns>
        public static NetworkService GetService()
        {
            if (!_isCreated)
            {
                throw new Exception("Le service n'a pas encore été créée");
            }
            return _networkServiceHost.NetworkGameService;
        }

        /// <summary>
        /// Permet de récupérer l'hôte qui hébérge le service qui doit déjà être créer
        /// </summary>
        /// <returns></returns>
        public static NetworkServiceHost GetNetworkServiceHost()
        {
            if (!_isCreated)
            {
                throw new Exception("L'hôte de service n'a pas encore été créée");
            }
            return _networkServiceHost;
        }

        /// <summary>
        /// Permet d'ouvrir le service hébérgé par l'hôte
        /// </summary>
        public static void Open()
        {
            if (!_isCreated)
            {
                throw new Exception("L'hôte de service n'a pas encore été créée");
            }
            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            _networkServiceHost._host.Open();
        }

        /// <summary>
        /// Permet de fermer le service hébérgé par l'hôte
        /// </summary>
        public static void Close()
        {
            if (!_isCreated)
            {
                throw new Exception("L'hôte de service n'a pas encore été créée");
            }
            _networkServiceHost._host.Close();
        }
    }
}