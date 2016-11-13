using System;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.Model
{
    [Serializable]
    public class Square
    {
        public Piece.Piece Piece { get; set; } = null;
        public int X { get; }
        public int Y { get; }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
