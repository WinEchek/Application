using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Model
{
    [Serializable]
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
                    Squares[i, j] = new Square(i, j);
                }
            }
            //TODO Move piece placement logic appart from the board
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j].Piece = ((i+j) %2 ==0) ? new Bishop(Color.Black, Squares[i,j]) : new Bishop(Color.White, Squares[i, j]);
                }
            }
        }
    }
}
