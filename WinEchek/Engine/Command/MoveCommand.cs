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
        private Coordinate _startCoordinate;
        private Coordinate _targetCoordinate;

        private Square _startSquare;
        private Square _targetSquare;

        private Piece _piece;
        private Piece _removedPiece;

        private bool _hasChangedState;


        /// <summary>
        /// Board
        /// </summary>
        /// <value>
        /// The board the command is working on
        /// </value>
        public Board Board { get; set; }


        /// <summary>
        /// MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        public MoveCommand(Move move)
        {
            Board = move.StartSquare.Board;

            _startCoordinate.X = move.StartSquare.X;
            _startCoordinate.Y = move.StartSquare.Y;

            _targetCoordinate.X = move.TargetSquare.X;
            _targetCoordinate.Y = move.TargetSquare.Y;  
        }

        private MoveCommand(MoveCommand command, Board board)
        {
            Board = board;
            _startCoordinate = command._startCoordinate;
            _targetCoordinate = command._targetCoordinate;
        }

        /// <summary>
        /// Execute the move on the Board
        /// </summary>
        public void Execute()
        {
            _targetSquare = Board.Squares[_targetCoordinate.X, _targetCoordinate.Y];
            _startSquare = Board.Squares[_startCoordinate.X, _startCoordinate.Y];
            _piece = _startSquare.Piece;

            //Has moved update
            if (!_piece.HasMoved)
            {
                _piece.HasMoved = true;
                _hasChangedState = true;
            }

            //Square is empty of piece
            if (_targetSquare.Piece == null)
            {
                _startSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            //There is a taken piece
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

        public Color PieceColor => _piece.Color;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() => _piece + " de " + _startSquare + " vers " + _targetSquare;
    }
}