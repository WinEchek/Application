using System;
using WinEchek.Model.Pieces;

namespace WinEchek.Model
{
    [Serializable]
    public class Board
    {
        public Board(int size = 8)
        {
            Size = size;

            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    Squares[i, j] = new Square(this, i, j);

            EightByEightInit();
            //PatTestInit();
        }

        /// <summary>
        ///     Copy constructor
        /// </summary>
        /// <param name="board">Board to copy</param>
        public Board(Board board)
        {
            Size = board.Size;

            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    Square square = new Square(this, i, j);
                    square.Piece = board.Squares[i, j]?.Piece?.Clone(square);
                    Squares[i, j] = square;
                }
        }

        public int Size { get; }
        public Square[,] Squares { get; }

        #region ConvenienceGetters

        /// <summary>
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The square at the coordinate</returns>
        public Square SquareAt(Coordinate coordinate) => Squares[coordinate.X, coordinate.Y];

        /// <summary>
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The piece at the coordinate</returns>
        public Piece PieceAt(Coordinate coordinate) => SquareAt(coordinate).Piece;

        #endregion

        #region BoardInits

        private void EightByEightInit()
        {
            //Pions noirs
            for (int i = 0; i < Size; i++)
                Squares[i, 1].Piece = new Pawn(Color.Black, Squares[i, 1]);

            Squares[0, 0].Piece = new Rook(Color.Black, Squares[0, 0]);
            Squares[1, 0].Piece = new Knight(Color.Black, Squares[1, 0]);
            Squares[2, 0].Piece = new Bishop(Color.Black, Squares[2, 0]);
            Squares[3, 0].Piece = new Queen(Color.Black, Squares[3, 0]);
            Squares[4, 0].Piece = new King(Color.Black, Squares[4, 0]);
            Squares[5, 0].Piece = new Bishop(Color.Black, Squares[5, 0]);
            Squares[6, 0].Piece = new Knight(Color.Black, Squares[6, 0]);
            Squares[7, 0].Piece = new Rook(Color.Black, Squares[7, 0]);

            //Pions blancs
            for (int i = 0; i < Size; i++)
                Squares[i, 6].Piece = new Pawn(Color.White, Squares[i, 6]);

            Squares[0, 7].Piece = new Rook(Color.White, Squares[0, 7]);
            Squares[1, 7].Piece = new Knight(Color.White, Squares[1, 7]);
            Squares[2, 7].Piece = new Bishop(Color.White, Squares[2, 7]);
            Squares[3, 7].Piece = new Queen(Color.White, Squares[3, 7]);
            Squares[4, 7].Piece = new King(Color.White, Squares[4, 7]);
            Squares[5, 7].Piece = new Bishop(Color.White, Squares[5, 7]);
            Squares[6, 7].Piece = new Knight(Color.White, Squares[6, 7]);
            Squares[7, 7].Piece = new Rook(Color.White, Squares[7, 7]);
        }

        private void PatTestInit()
        {
            Squares[7, 0].Piece = new King(Color.Black, Squares[7, 0]);
            Squares[0, 0].Piece = new King(Color.White, Squares[0, 0]);
            Squares[2, 5].Piece = new Rook(Color.White, Squares[2, 5]);
            Squares[3, 5].Piece = new Rook(Color.White, Squares[3, 5]);
        }

        #endregion
    }
}