using System.ServiceModel;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NetworkService : INetworkService
    {
        public string ClientAdress { get; set; }
        public Color PlayerColor { get; set; }

        public delegate void MoveReceivedHandler(Move move);
        public event MoveReceivedHandler MoveReceived;

        public delegate void ClientUriReceivedHandler(string uri);
        public event ClientUriReceivedHandler ClientUriReceived;


        public void Inform(Move move)
        {
            MoveReceived?.Invoke(move);
        }

        public void SendClientAdress(string uri)
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

        public string GetColor()
        {
            return PlayerColor == Color.White ? "White" : "Black";
        }
    }
}