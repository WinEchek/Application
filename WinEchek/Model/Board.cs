using System.Windows.Media;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.Model
{
    public class Board
    {
        public const int Size = 8;
        public Square[,] Squares { get; }

        public Board()
        {
            Squares = new Square[8,8];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j] = new Square(((i+j+1) % 2 == 0) ? Colors.SaddleBrown : Colors.Beige, i, j);
                }
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j].Piece = new King(Color.Black);
                }
            }
        }
    }
}
