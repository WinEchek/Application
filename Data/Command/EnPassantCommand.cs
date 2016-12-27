using System;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.Command
{
    [Serializable]
    public class EnPassantCommand : ICompensableCommand
    {
        private ICompensableCommand _firstMove;
        private ICompensableCommand _secondMove;


        public EnPassantCommand(Move move, Board board)
        {
            Move = move;

            bool isWhite = move.PieceColor == Color.White;
            bool isLeft = move.StartCoordinate.X > move.TargetCoordinate.X;

            int x = move.StartCoordinate.X + (isLeft ? -1 : 1);
            int y = move.StartCoordinate.Y;

            Square startSquare = board.SquareAt(move.StartCoordinate);
            Square secondSquare = board.Squares[x, y];
            Square thirdSquare = board.Squares[x, y + (isWhite ? -1 : 1)];

            _firstMove = new MoveCommand(new Move(startSquare, secondSquare, Move.PieceType, Move.PieceColor), board);
            _secondMove = new MoveCommand(new Move(secondSquare, thirdSquare, Move.PieceType, Move.PieceColor), board);
        }

        private EnPassantCommand(EnPassantCommand command, Board board)
        {
            Move = command.Move;
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

        public bool TakePiece => true;

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new EnPassantCommand(this, board);

        public override string ToString() => "En passant de " + Move.StartCoordinate + " vers " + Move.TargetCoordinate;
    }
}