using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.Model
{
    public class Square
    {
        public Piece.Piece Piece { get; set; }
        public Color Color { get; set; }
        public int X { get; }
        public int Y { get; }

        public Square(Color color, int x, int y)
        {
            Color = color;
            X = x;
            Y = y;
        }
    }
}
