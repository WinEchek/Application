using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Bishop : Piece
    {
        public Bishop(Color color, Square square) : base(color, square)
        {
            base.Type = Type.Bishop;
        }
        public override string ToString() => "Fou";
    }
}
