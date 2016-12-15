using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Queen : Piece
    {
        public Queen(Color color, Square square) : base(color, square)
        {
            Type = Type.Queen;
        }

        public override string ToString() => "Reine";
    }
}
