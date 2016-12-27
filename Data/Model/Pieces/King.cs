using System;

namespace WinEchek.Model.Pieces
{
    [Serializable]
    public class King : Piece
    {
        public King(Color color, Square square) : base(color, square)
        {
            Type = Type.King;
        }

        public King(Color color) : base(color)
        {
            Type = Type.King;
        }

        public override Piece Clone(Square square) => new King(Color, square);

        public override string ToString() => "Roi";
    }
}