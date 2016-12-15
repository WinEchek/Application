using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Pawn : Piece
    {
        public Pawn(Color color, Square square) : base(color, square)
        {
            Type = Type.Pawn;
        }

        public override Piece Clone(Square square) => new Pawn(Color, square);


        public override string ToString() => "Pion";
    }
}
