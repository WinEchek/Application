using System;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    internal class CastlingCommand : ICompensableCommand
    {
        private Move _move;
        private ICompensableCommand _kingCommand;
        private ICompensableCommand _rookCommand;

        public CastlingCommand(Move move)
        {
            Board board = move.StartSquare.Board;
            _move = move;
            if (move.TargetSquare.X == 0)
            {
                _kingCommand = new MoveCommand(new Move(move.Piece, board.Squares[2, move.StartSquare.Y]));
                _rookCommand = new MoveCommand(new Move(move.TargetSquare.Piece, board.Squares[3, move.StartSquare.Y]));
            }
            else
            {
                _kingCommand = new MoveCommand(new Move(move.Piece, board.Squares[6, move.StartSquare.Y]));
                _rookCommand = new MoveCommand(new Move(move.TargetSquare.Piece, board.Squares[5, move.StartSquare.Y]));
            }
        }

        private CastlingCommand(CastlingCommand command, Board board)
        {
            _move = command._move;
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
            _rookCommand.Compensate();
            _kingCommand.Compensate();
        }

        public Type PieceType => _move.Piece.Type;

        public ICompensableCommand Copy(Board board) => new CastlingCommand(this, board);

        public override string ToString() => "Roc vers tour " + _move.TargetSquare;
    }
}