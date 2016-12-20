using System;
using System.ServiceModel;
using WinEchek.Model;

namespace WinEchek.Core.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NetworkGameService : INetworkGameService
    {
        public Uri ClientAdress { get; set; }

        public delegate void MoveReceivedHandler(Move move);
        public event MoveReceivedHandler MoveReceived;

        public delegate void ClientUriReceivedHandler();
        public event ClientUriReceivedHandler ClientUriReceived;


        public void Inform(Move move)
        {
            MoveReceived?.Invoke(move);
        }

        public void SendClientAdress(Uri uri)
        {
            // On sauvegarde l'adresse qu'on a reçut
            ClientAdress = uri;

            // On informe qu'on à reçut les informations de connexion
            ClientUriReceived?.Invoke();
        }
    }
}