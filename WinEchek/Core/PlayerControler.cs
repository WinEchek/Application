using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public abstract class PlayerControler
    {
        protected PlayerControler(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }
        public bool IsPlayable { get; set; }
        public abstract void Play();

        public abstract List<Square> PossibleMoves(Piece piece);
    }
}