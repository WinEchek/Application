using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public class RealPlayer : IPlayer
    {
        public Game Game { get; set; }
        public Color Color { get; set; }
        public RealPlayer(Game game, Color color)
        {
            Game = game;
            Color = color;
        }
        public void DoMove(Piece piece, Square square)
        {
            Game.DoMove(piece, square);
        }
    }
}
