using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using JetBrains.Annotations;
using WinEchek.Model;

namespace WinEchek.Network
{
    public class NetworkGameServiceClient : ClientBase<INetworkGameService>, INetworkGameService
    {
        public NetworkGameServiceClient([NotNull] Binding binding, [NotNull] EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }

        public void Inform(Move move)
        {
            Channel.Inform(move);
        }

        public void SendClientAdress(Uri uri)
        {
            Channel.SendClientAdress(uri);
        }

        public string Echo(string message)
        {
            return Channel.Echo(message);
        }
    }
}