using System;
using System.ServiceModel;
using WinEchek.Model;

namespace WinEchek.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NetworkGameService : INetworkGameService
    {
        public delegate void ClientUriReceivedHandler(Uri uri);

        public delegate void MoveReceivedHandler(Move move);

        public Uri ClientAdress { get; set; }


        public void Inform(Move move)
        {
            MoveReceived?.Invoke(move);
        }

        public void SendClientAdress(Uri uri)
        {
            // On sauvegarde l'adresse qu'on a reçut
            ClientAdress = uri;

            // On informe qu'on à reçut les informations de connexion
            ClientUriReceived?.Invoke(uri);
        }

        public string Echo(string message)
        {
            return message;
        }

        public event MoveReceivedHandler MoveReceived;
        public event ClientUriReceivedHandler ClientUriReceived;
    }
}