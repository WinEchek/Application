using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    internal class EnPassantCommand : ICompensableCommand
    {
        private Move _move;
        private ICompensableCommand _firstMove;
        private ICompensableCommand _secondMove;


        public EnPassantCommand(Move move, Board board)
        {
            _move = move;

            bool isWhite = move.PieceColor == Color.White;
            bool isLeft = move.StartCoordinate.X > move.TargetCoordinate.X;

            int x = move.StartCoordinate.X + (isLeft ? -1 : 1);
            int y = move.StartCoordinate.Y;

            Square startSquare = board.SquareAt(move.StartCoordinate);
            Square secondSquare = board.Squares[x, y];
            Square thirdSquare = board.Squares[x, y + (isWhite ? -1 : 1)];

            _firstMove = new MoveCommand(new Move(startSquare, secondSquare, _move.PieceType, _move.PieceColor), board);
            _secondMove = new MoveCommand(new Move(secondSquare, thirdSquare, _move.PieceType, _move.PieceColor), board);
        }

        private EnPassantCommand(EnPassantCommand command, Board board)
        {
            _move = command._move;
            _firstMove = command._firstMove.Copy(board);
            _secondMove = command._secondMove.Copy(board);
        }

        public void Execute()
        {
            _firstMove.Execute();
            _secondMove.Execute();
        }

        public void Compensate()
        {
            _secondMove.Compensate();
            _firstMove.Compensate();
        }

        public Type PieceType => _move.PieceType;

        public Color PieceColor => _move.PieceColor;

        public ICompensableCommand Copy(Board board) => new EnPassantCommand(this, board);

        public override string ToString() => "En passant de " + _move.StartCoordinate + " vers " + _move.TargetCoordinate;
    }
}