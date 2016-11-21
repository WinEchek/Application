using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class RookMovementRule : IRule
    {
        public bool IsMoveValid(Move move)
        {
            Board board = move.Square.Board;
            Square square = move.Square;
            Piece piece = move.Piece;
            //if the movement is not inline
            if (!(piece.Square.X == square.X ^ piece.Square.Y == square.Y)) return false;

            List<Square> line = new List<Square>();

            //Move along X
            if (piece.Square.Y == square.Y)
            {
                line =
                    board.Squares.OfType<Square>()
                        .ToList()
                        .Where(x => Between(piece.Square.X, square.X, x.X) && x.Y == square.Y)
                        .ToList();
            }
            else
            {
                line =
                    board.Squares.OfType<Square>()
                        .ToList()
                        .Where(x => Between(piece.Square.Y, square.Y, x.Y) && x.X == square.X)
                        .ToList();
            }
            foreach (Square square1 in line)
            {
                Console.WriteLine(square1.Piece);
            }

            return line.All(betweenSquare => betweenSquare.Piece == null);
        }

        private bool Between(int i, int j, int x)
        {
            if (i > j)
                return i > x && j < x;
            else
                return j > x && i < x;
        }
    }
}