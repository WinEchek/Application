using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    public class PromoteCommand : ICompensableCommand
    {
        private Board _board;

        private Move _move;

        public PromoteCommand(Move move)
        {
            if(move?.PromotePieceType == null) throw new ArgumentNullException();
            _board = move.Board;
            _move = move;
        }

        private PromoteCommand(PromoteCommand promoteCommand, Board board)
        {
            _move = promoteCommand._move;
            _board = board;
        }

        public void Execute()
        {
            _board.Squares[_move.StartSquare.X, _move.StartSquare.Y].Piece = null;
            Piece piece;
            Square square = _board.Squares[_move.TargetSquare.X, _move.TargetSquare.Y];
            switch (_move.PromotePieceType)
            {
                case Type.Bishop:
                    piece = new Bishop(_move.PieceColor, square);
                    break;
                case Type.King:
                    piece = new King(_move.PieceColor, square);
                    break;
                case Type.Queen:
                    piece = new Queen(_move.PieceColor, square);
                    break;
                case Type.Pawn:
                    piece = new Pawn(_move.PieceColor, square);
                    break;
                case Type.Knight:
                    piece = new Knight(_move.PieceColor, square);
                    break;
                case Type.Rook:
                    piece = new Rook(_move.PieceColor, square);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            square.Piece = piece;
        }

        public void Compensate()
        {
            _board.Squares[_move.TargetSquare.X, _move.TargetSquare.Y].Piece = null;
            _board.Squares[_move.StartSquare.X, _move.StartSquare.Y].Piece = new Pawn(_move.PieceColor, _board.Squares[_move.StartSquare.X, _move.StartSquare.Y]);
        }

        public Type PieceType => _move.PieceType;

        public Color PieceColor => _move.PieceColor;

        public ICompensableCommand Copy(Board board) => new PromoteCommand(this, board);
    }
}