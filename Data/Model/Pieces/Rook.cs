using System;

namespace WinEchek.Model.Pieces
{
    [Serializable]
    public class Rook : Piece
    {
        public Rook(Color color, Square square) : base(color, square)
        {
            Type = Type.Rook;
        }

        public Rook(Color color) : base(color)
        {
            Type = Type.Rook;
        }

        public override Piece Clone(Square square) => new Rook(Color, square);
        public override string ToString() => "Tour";
    }
}