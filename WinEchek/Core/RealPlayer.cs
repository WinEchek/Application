using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public class RealPlayer : IPlayer
    {
        //public TaskCompletionSource<Move> MakeMoveCompletionSource = new TaskCompletionSource<Move>();
        public Game Game { get; set; }
        public Color Color { get; set; }
        public Move MoveToMake { get; set; }
        public RealPlayer(Game game, Color color)
        {
            Game = game;
            Color = color;
        }

        public Move Play()
        {
            while (MoveToMake == null) ;
            Move res = MoveToMake;
            MoveToMake = null;
            //MakeMoveCompletionSource = new TaskCompletionSource<Move>();
            return res;
        }
    }
}
