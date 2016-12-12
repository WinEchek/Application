using System;

namespace WinEchek.Model
{
    [Serializable]
    public class Move
    {
        public Piece.Piece Piece { get; }
        public Square StartSquare { get; }
        public Square TargetSquare { get; }

        public Move(Piece.Piece piece, Square square)
        {
            Piece = piece;
            StartSquare = piece.Square;
            TargetSquare = square;
        }
    }
}