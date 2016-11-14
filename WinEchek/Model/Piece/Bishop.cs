using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Bishop : Piece
    {
        public Bishop(Color color, Square square) : base(color, square)
        {
        }

        public override Type Type()
        {
            return Model.Piece.Type.Bishop;
        }
    }
}
