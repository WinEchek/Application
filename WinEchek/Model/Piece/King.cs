using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class King : Piece
    {
        public King(Color color, Square square) : base(color, square)
        {
            Type = Type.King;
        }

        public override Piece Clone(Square square) => new King(Color, square);

        public override string ToString() => "Roi";
    }
}
