using System;

namespace WinEchek.Model.Pieces
{
    [Serializable]
    public class Bishop : Piece
    {
        public Bishop(Color color, Square square) : base(color, square)
        {
            Type = Type.Bishop;
        }

        public Bishop(Color color) : base(color)
        {
            Type = Type.Bishop;
        }

        public override Piece Clone(Square square) => new Bishop(Color, square);

        public override string ToString() => "Fou";
    }
}