using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    internal class CastlingCommand : ICompensableCommand
    {
        private ICompensableCommand _kingCommand;
        private ICompensableCommand _rookCommand;
        

        public CastlingCommand(Move move, Board board)
        {
            Move = move;

            bool isLeftCastling = move.TargetCoordinate.X == 0;

            _kingCommand = new MoveCommand(new Move(board.PieceAt(move.StartCoordinate), board.Squares[isLeftCastling ? 2 : 6, move.StartCoordinate.Y]), board);
            _rookCommand = new MoveCommand(new Move(board.PieceAt(move.TargetCoordinate), board.Squares[isLeftCastling ? 3 : 5, move.TargetCoordinate.Y]), board);
        }

        private CastlingCommand(CastlingCommand command, Board board)
        {
            Move = command.Move;

            _rookCommand = command._rookCommand.Copy(board);
            _kingCommand = command._kingCommand.Copy(board);
        }

        public void Execute()
        {
            _rookCommand.Execute();
            _kingCommand.Execute();
        }

        public void Compensate()
        {
            _kingCommand.Compensate();
            _rookCommand.Compensate();
        }

        public bool TakePiece => false;

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new CastlingCommand(this, board);

        public override string ToString() => "Roc vers tour " + Move.TargetCoordinate;
    }
}