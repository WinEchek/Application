using System;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.Command
{
    /// <summary>
    ///     Represents a Move to execute on the model
    /// </summary>
    [Serializable]
    public class MoveCommand : ICompensableCommand
    {
        private Board _board;

        private bool _hasChangedState;
        private Piece _piece;
        private Piece _removedPiece;
        private Square _startSquare;
        private Square _targetSquare;


        /// <summary>
        ///     MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <param name="board">The board the command executes on</param>
        public MoveCommand(Move move, Board board)
        {
            Move = move;
            _board = board;

            TakePiece = board.PieceAt(Move.TargetCoordinate) != null;
        }

        private MoveCommand(MoveCommand command, Board board)
        {
            _board = board;
            Move = command.Move;

            TakePiece = board.PieceAt(Move.TargetCoordinate) != null;
        }

        /// <summary>
        ///     Execute the move on the Board
        /// </summary>
        public void Execute()
        {
            _targetSquare = _board.SquareAt(Move.TargetCoordinate);
            _startSquare = _board.SquareAt(Move.StartCoordinate);
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
        ///     Undo the move
        /// </summary>
        public void Compensate()
        {
            if (_hasChangedState) _piece.HasMoved = false;

            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public bool TakePiece { get; }

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() => _piece + " de " + Move.StartCoordinate + " vers " + Move.TargetCoordinate;
    }
}