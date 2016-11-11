using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WinEchek.Model
{
    public class Square
    {
        public Piece.Piece piece { get; set; }
        public Color Color { get; set; }

        public Square(Color color)
        {
            Color = color;
        }
    }
}
