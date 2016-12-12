using System;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Command
{
    /// <summary>
    /// Represents a Move to execute on the model
    /// </summary>
    [Serializable]
    public class MoveCommand : ICompensableCommand
    {
        private Piece _piece;
        private Piece _removedPiece;
        private Square _startSquare;
        private Square _targetSquare;

        public Move Move { get; set; }

        /// <summary>
        /// MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        public MoveCommand(Move move)
        {
            Move = move;
            _piece = move.Piece;
            _startSquare = move.StartSquare;
            _targetSquare = move.TargetSquare;
        }

        /// <summary>
        /// Execute the move on the model
        /// </summary>
        public void Execute()
        {
            _startSquare = _piece.Square;

            //Si case vide
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
            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public override string ToString() => _piece + " de " + _startSquare + " vers " + _targetSquare;
    }
}