using System;
using System.ServiceModel;
using WinEchek.Model;

namespace WinEchek.Core.Network
{
    [ServiceContract]
    public interface INetworkGameService
    {
        [OperationContract(IsOneWay = true)]
        void Inform(Move move);

        /// <summary>
        /// Méthode pour envoyer au service les informations pour se connecter au nôtre
        /// </summary>
        /// <param name="uri"></param>
        [OperationContract(IsOneWay = true)]
        void SendClientAdress(Uri uri);

        [OperationContract]
        string Echo(string message);
    }
}