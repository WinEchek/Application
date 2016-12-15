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

        public override string ToString() => "Roi";
    }
}
