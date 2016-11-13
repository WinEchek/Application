using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Rook : Piece
    {
        public Rook(Color color, Square square) : base(Type.Rook, color, square)
        {
        }
    }
}
