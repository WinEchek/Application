using System;
using System.Windows;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    [Serializable]
    internal class EnPassantCommand : ICompensableCommand
    {
        private Piece _piece;
        private Move _move;
        private ICompensableCommand _firstMove;
        private ICompensableCommand _secondMove;
 


        public EnPassantCommand(Move move)
        {
            _move = move;
            _piece = move.Piece;
            Board board = move.StartSquare.Board;

            bool isWhite = move.Piece.Color == Color.White;
            bool isLeft = move.StartSquare.X > move.TargetSquare.X;

            int x = move.StartSquare.X + (isLeft ? -1 : 1);
            int y = move.StartSquare.Y;

            Square startSquare = move.StartSquare;
            Square secondSquare = board.Squares[x, y];
            Square thirdSquare = board.Squares[x, y + (isWhite ? -1 : 1)];

            _firstMove = new MoveCommand(new Move(startSquare, secondSquare, _move.PieceType, _move.PieceColor));
            _secondMove = new MoveCommand(new Move(secondSquare, thirdSquare, _move.PieceType, _move.PieceColor));

            /** Old version
            if (move.StartSquare.X > move.TargetSquare.X)
            {
                if (move.Piece.PieceColor == PieceColor.White)
                {
                    _firstMove =
                        new MoveCommand(new Move(move.Piece, board.Squares[move.StartSquare.X - 1, move.StartSquare.Y]));

                    _secondMove = new MoveCommand(new Move(board.Squares[move.StartSquare.X - 1, move.StartSquare.Y], board.Squares[move.StartSquare.X - 1, move.StartSquare.Y - 1]));
                }
                else
                {
                    _firstMove =
                        new MoveCommand(new Move(move.Piece, board.Squares[move.StartSquare.X - 1, move.StartSquare.Y]));
                    _secondMove = new MoveCommand(new Move(board.Squares[move.StartSquare.X - 1, move.StartSquare.Y], board.Squares[move.StartSquare.X - 1, move.StartSquare.Y + 1]));
                }
            }
            else
            {
                if (move.Piece.PieceColor == PieceColor.White)
                {
                    _firstMove =
                        new MoveCommand(new Move(move.Piece, board.Squares[move.StartSquare.X + 1, move.StartSquare.Y]));
                    _secondMove = new MoveCommand(new Move(board.Squares[move.StartSquare.X + 1, move.StartSquare.Y], board.Squares[move.StartSquare.X + 1, move.StartSquare.Y - 1]));
                }
                else
                {
                    _firstMove =
                        new MoveCommand(new Move(move.Piece, board.Squares[move.StartSquare.X + 1, move.StartSquare.Y]));
                    _secondMove = new MoveCommand(new Move(board.Squares[move.StartSquare.X + 1, move.StartSquare.Y], board.Squares[move.StartSquare.X + 1, move.StartSquare.Y + 1]));
                }
            }
            **/
        }

        private EnPassantCommand(EnPassantCommand command, Board board)
        {
            _move = command._move;
            _piece = _move.Piece;
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

        public Type PieceType => _piece.Type;

        public Color PieceColor => _piece.Color;

        public ICompensableCommand Copy(Board board) => new EnPassantCommand(this, board);

        public override string ToString() => "En passant de " + _move.StartSquare + " vers " + _move.TargetSquare;
    }
}