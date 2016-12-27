using System;

namespace WinEchek.Model.Pieces
{
    [Serializable]
    public class Pawn : Piece
    {
        public Pawn(Color color, Square square) : base(color, square)
        {
            Type = Type.Pawn;
        }

        public Pawn(Color color) : base(color)
        {
            Type = Type.Pawn;
        }

        public bool EnPassant { get; set; }

        public override Piece Clone(Square square) => new Pawn(Color, square);

        public override string ToString() => "Pion";
    }
}