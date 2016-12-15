using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Bishop : Piece
    {
        public Bishop(Color color, Square square) : base(color, square)
        {
        }

        public override Piece Clone(Square square) => new Bishop(Color, square);

        public override Type Type => Type.Bishop;
        public override string ToString() => "Fou";
    }
}
