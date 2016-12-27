using System;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.Command
{
    [Serializable]
    public class PromoteCommand : ICompensableCommand
    {
        private Board _board;
        private ICompensableCommand _moveCommand;
        private Piece _oldPawn;

        public PromoteCommand(Move move, Board board)
        {
            if (move?.PromotePieceType == null)
                throw new NullReferenceException("Can't build a promote command with null Move.PromotedPieceType");
            _board = board;
            Move = move;

            _moveCommand = new MoveCommand(move, board);
            _oldPawn = board.PieceAt(move.StartCoordinate);
        }

        private PromoteCommand(PromoteCommand promoteCommand, Board board)
        {
            Move = promoteCommand.Move;
            _board = board;
            _moveCommand = promoteCommand._moveCommand.Copy(board);
            _oldPawn = board.PieceAt(Move.StartCoordinate);
        }

        public void Execute()
        {
            _moveCommand.Execute();

            Square square = _board.SquareAt(Move.TargetCoordinate);
            Piece piece;
            switch (Move.PromotePieceType)
            {
                case Type.Bishop:
                    piece = new Bishop(Move.PieceColor, square);
                    break;
                case Type.King:
                    piece = new King(Move.PieceColor, square);
                    break;
                case Type.Queen:
                    piece = new Queen(Move.PieceColor, square);
                    break;
                case Type.Pawn:
                    piece = new Pawn(Move.PieceColor, square);
                    break;
                case Type.Knight:
                    piece = new Knight(Move.PieceColor, square);
                    break;
                case Type.Rook:
                    piece = new Rook(Move.PieceColor, square);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            square.Piece = piece;
        }

        public void Compensate()
        {
            _board.SquareAt(Move.TargetCoordinate).Piece = _oldPawn;
            _moveCommand.Compensate();
        }

        public bool TakePiece => _moveCommand.TakePiece;

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new PromoteCommand(this, board);

        public override string ToString() =>
            "Promotion en " + Move.PromotePieceType + " en " + Move.TargetCoordinate;
    }
}