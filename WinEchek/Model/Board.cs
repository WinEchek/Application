using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Model
{
    public class Board
    {
        public const int Size = 8;
        public Square[,] Squares { get; }

        public Board()
        {
            Squares = new Square[Size,Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j] = new Square(((i+j) % 2 == 0) ? Color.White : Color.Black, i, j);
                }
            }
            //TODO Move piece placement logic appart from the board
            Piece.Piece[] Pieces = {new Bishop(Piece.Color.Black), new King(Piece.Color.White), new Queen(Piece.Color.Black), new Pawn(Piece.Color.White), new Rook(Piece.Color.Black), new Knight(Piece.Color.White) };
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j].Piece = Pieces[(i + j)%6];
                }
            }
        }
    }
}
