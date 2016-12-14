using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    /// <summary>
    /// Represents a Move to execute on the model
    /// </summary>
    [Serializable]
    public class MoveCommand : ICompensableCommand
    {
        [Serializable]
        private struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private Point _piecePoint;
        private Point _startPoint;
        private Point _targetPoint;

        private Square _startSquare;
        private Square _targetSquare;
        private Piece _piece;
        private Piece _removedPiece;

        private bool _hasChangedState = false;

        public Board Board { get; set; }


        /// <summary>
        /// MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        public MoveCommand(Move move)
        {
            Board = move.StartSquare.Board;

            _startSquare = move.StartSquare;
            _targetSquare = move.TargetSquare;
            _piece = move.Piece;

            _piecePoint.X = move.Piece.Square.X;
            _piecePoint.Y = move.Piece.Square.Y;

            _startPoint.X = move.StartSquare.X;
            _startPoint.Y = move.StartSquare.Y;

            _targetPoint.X = move.TargetSquare.X;
            _targetPoint.Y = move.TargetSquare.Y;
        }

        public MoveCommand(MoveCommand command, Board board)
        {
            Board = board;
            _piecePoint = command._piecePoint;
            _startPoint = command._startPoint;
            _targetPoint = command._targetPoint;

            _targetSquare = Board.Squares[_targetPoint.X, _targetPoint.Y];
            _startSquare = Board.Squares[_startPoint.X, _startPoint.Y];
            _piece = _startSquare.Piece;
        }

        /// <summary>
        /// Execute the move on the model
        /// </summary>
        public void Execute()
        {
            //Si case vide
            if (!_piece.HasMoved)
            {
                _piece.HasMoved = true;
                _hasChangedState = true;
            }
            if (_targetSquare.Piece == null)
            {
                _startSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            //Si il y à une pièce à prendre
            else
            {
                _removedPiece = _targetSquare.Piece;
                _targetSquare.Piece = null;
                _piece.Square.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
        }

        /// <summary>
        /// Undo the move
        /// </summary>
        public void Compensate()
        {
            if (_hasChangedState) _piece.HasMoved = false;
            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public Type PieceType => _piece.Type;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() => _piece + " de " + _startSquare + " vers " + _targetSquare;
    }
}