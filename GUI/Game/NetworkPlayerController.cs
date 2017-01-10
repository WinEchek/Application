using System;
using System.Collections.Generic;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.Network;

namespace WinEchek.Game
{
    public class NetworkPlayerController : PlayerControler
    {
        public NetworkServiceHost NetworkServiceHost { get; set; }
        public INetworkService Client { get; set; }

        public NetworkPlayerController(NetworkServiceHost networkServiceHost)
        {
            NetworkServiceHost = networkServiceHost;
            Client = NetworkServiceClient.Channel();

            /** Quand notre service reçoit le mouvement du joueur graphique de l'autre jeu dans ce cas,
             *  on l'envoie comme notre mouvment étant donné qu'on représente ce joueur dans notre partie
             */
            networkServiceHost.NetworkGameService.MoveReceived += NetworkGameServiceOnMoveReceived;
        }

        private void NetworkGameServiceOnMoveReceived(Move move)
        {
            // Le coup qu'on reçoit a déjà été validé donc il est censé être correct...
            Move(move);
        }

        public override void Play(Move move)
        {
            if (move == null)
                return;
            // On informe l'adversaire du coup que notre joueur local vient de réaliser
            try
            {
                Client.Inform(move);
            }
            catch (Exception e)
            {
                Player.LeaveGame(e.Message);
            }
            
        }

        public override void Move(Move move)
        {
            Player.Move(move);
        }

        public override void InvalidMove(List<string> reasonsList)
        {
            throw new System.NotImplementedException();
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            throw new System.NotImplementedException();
        }

        public override void Stop()
        {

        }
    }
}
