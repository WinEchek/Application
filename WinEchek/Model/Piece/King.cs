using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class King : Piece
    {
        public King(Color color, Square square) : base(color, square)
        {
        }

        public override Piece Clone(Square square) => new King(Color, square);


        public override Type Type => Type.King;
        public override string ToString() => "Roi";
    }
}
