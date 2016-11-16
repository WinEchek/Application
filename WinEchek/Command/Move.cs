using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Command
{
    public class Move : ICompensableCommand
    {
        private Piece _piece;
        private Piece _removedPiece = null;
        private Square _previousSquare;
        private Square _targetSquare;

        public Move(Piece piece, Square targetSquare)
        {
            _piece = piece;
            _targetSquare = targetSquare;
        }

        public void Execute()
        {
            _previousSquare = _piece.Square;

            if (_targetSquare.Piece == null)//Si case vide
            {
                _previousSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            else
            {
                _removedPiece = _targetSquare.Piece;
                _targetSquare.Piece = null;
                _piece.Square.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
        }

        public void Compensate()
        {
            _targetSquare.Piece = _removedPiece;
            _previousSquare.Piece = _piece;
            _piece.Square = _previousSquare;
        }
    }
}