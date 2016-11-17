using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Pawn : Piece
    {
        public Pawn(Color color, Square square) : base(color, square)
        {
        }

        public override Type Type => Type.Pawn;
    }
}
