using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public class RealPlayer : Player
    {
        private BoardView _boardView;

        public RealPlayer(Color color, BoardView boardView) : base(color)
        {
            _boardView = boardView;
            boardView.BoardMove += BoardViewOnBoardMove;
        }

        private void BoardViewOnBoardMove(Move move) => Move(move);

        public override void Play()
        {
            _boardView.Color = Color;
        }
    }
}
