using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Rook : Piece
    {
        public Rook(Color color, Square square) : base(color, square)
        {
        }

        public override Type Type => Type.Rook;
        public override string ToString() => "Tour";
    }
}
