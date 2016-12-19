using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    internal class CastlingCommand : ICompensableCommand
    {
        private Move _move;
        private Piece _piece;

        private ICompensableCommand _kingCommand;
        private ICompensableCommand _rookCommand;

        public CastlingCommand(Move move)
        {
            Board board = move.StartSquare.Board;
            _move = move;
            _piece = board.PieceAt();

            bool isLeftCastling = move.TargetSquare.X == 0;

            _kingCommand = new MoveCommand(new Move(move.Piece, board.Squares[isLeftCastling ? 2 : 6, move.StartSquare.Y]));
            _rookCommand = new MoveCommand(new Move(move.TargetSquare.Piece, board.Squares[isLeftCastling ? 3 : 5, move.TargetSquare.Y]));
        }

        private CastlingCommand(CastlingCommand command, Board board)
        {
            _move = command._move;
            _piece = _move.Piece;

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

        public Type PieceType => _piece.Type;

        public Color PieceColor => _piece.Color;

        public ICompensableCommand Copy(Board board) => new CastlingCommand(this, board);

        public override string ToString() => "Roc vers tour " + _move.TargetSquare;
    }
}