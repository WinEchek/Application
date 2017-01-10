using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WinEchek.Annotations;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core.Network
{
    public class NetworkServiceClient
    {
        private static INetworkService _instance;
        private static bool isCreated;

        private NetworkServiceClient()
        {
        }

        public static void Create(EndpointAddress uri)
        {
            NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None);
            ChannelFactory<INetworkService> channelFactory = new ChannelFactory<INetworkService>(netTcpBinding, uri);
            _instance = channelFactory.CreateChannel();
            isCreated = true;
        }

        public static INetworkService Channel()
        {
            if (!isCreated)
            {
                throw new Exception("Connexion avec le serveur non initialisé");
            }
            return _instance;
        }

    }
}