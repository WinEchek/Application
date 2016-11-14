using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Queen : Piece
    {
        public Queen(Color color, Square square) : base(color, square)
        {
        }

        public override Type Type()
        {
            return Model.Piece.Type.Queen;
        }
    }
}
