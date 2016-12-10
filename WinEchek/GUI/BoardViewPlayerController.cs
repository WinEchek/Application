using System.Collections.Generic;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.GUI
{
    public class BoardViewPlayerController : PlayerControler
    {
        private BoardView _boardView;

        public BoardViewPlayerController(Player player, BoardView boardView) : base(player)
        {
            _boardView = boardView;
        }

        public override void Play()
        {
            _boardView
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            throw new System.NotImplementedException();
        }
    }
}