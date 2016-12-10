using WinEchek.Core;
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
