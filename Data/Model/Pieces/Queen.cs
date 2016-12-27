using System;

namespace WinEchek.Model.Pieces
{
    [Serializable]
    public class Queen : Piece
    {
        public Queen(Color color, Square square) : base(color, square)
        {
            Type = Type.Queen;
        }

        public Queen(Color color) : base(color)
        {
            Type = Type.Queen;
        }

        public override Piece Clone(Square square) => new Queen(Color, square);

        public override string ToString() => "Reine";
    }
}