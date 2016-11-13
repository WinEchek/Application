using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Queen : Piece
    {
        public Queen(Color color, Square square) : base(Type.Queen, color, square)
        {
        }
    }
}
