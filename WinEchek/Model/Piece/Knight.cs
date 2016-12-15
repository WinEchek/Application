using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class Knight : Piece
    {
        public Knight(Color color, Square square) : base(color, square)
        {
            Type = Type.Knight;
        }

        public override Piece Clone(Square square) => new Knight(Color, square);

        public override string ToString() => "Cavalier";
    }
}
