using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Bishop : Piece
    {
        public Bishop(Color color, Square square) : base(Type.Bishop, color, square)
        {
        }
    }
}
