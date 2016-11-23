using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class QueenMovementRule : IRule
    {
        public bool IsMoveValid(Move move)
        {
            /**
             * X
             */ 
            List<Square> diagonalUpLeft = new List<Square>();
            List<Square> diagonalUpRight = new List<Square>();
            List<Square> diagonalDownLeft = new List<Square>();
            List<Square> diagonalDownRight = new List<Square>();

            bool diagonalUpLeftEnd = false;
            bool diagonalUpRightEnd = false;
            bool diagonalDownLeftEnd = false;
            bool diagonalDownRightEnd = false;



            Board board = move.Piece.Square.Board;

            for (int i = 1; i < 8; i++) {
                if (move.Piece.Square.X - i < 8 && move.Piece.Square.X - i >= 0 && move.Piece.Square.Y - i < 8 &&
                    move.Piece.Square.Y - i >= 0 && !diagonalUpLeftEnd) {
                    Square square = board.Squares[move.Piece.Square.X - i, move.Piece.Square.Y - i];
                    diagonalUpLeft.Add(square);
                    if (square.Piece != null) {
                        if (move.Piece.Color == square.Piece.Color) {
                            diagonalUpLeft.Remove(square);
                            // on supprime si la case contient une pièce de la même couleur que celle concernée par le déplacement
                        }
                        diagonalUpLeftEnd = true;
                    }
                }
                if (move.Piece.Square.X + i < 8 && move.Piece.Square.X + i >= 0 && move.Piece.Square.Y - i < 8 &&
                    move.Piece.Square.Y - i >= 0 && !diagonalUpRightEnd) {
                    Square square = board.Squares[move.Piece.Square.X + i, move.Piece.Square.Y - i];
                    diagonalUpRight.Add(square);
                    if (square.Piece != null) {
                        if (move.Piece.Color == square.Piece.Color) {
                            diagonalUpRight.Remove(square);
                            // on supprime si la case contient une pièce de la même couleur que celle concernée par le déplacement
                        }
                        diagonalUpRightEnd = true;
                    }
                }
                if (move.Piece.Square.X - i < 8 && move.Piece.Square.X - i >= 0 && move.Piece.Square.Y + i < 8 &&
                    move.Piece.Square.Y + i >= 0 && !diagonalDownLeftEnd) {
                    Square square = board.Squares[move.Piece.Square.X - i, move.Piece.Square.Y + i];
                    diagonalDownLeft.Add(square);
                    if (square.Piece != null) {
                        if (move.Piece.Color == square.Piece.Color) {
                            diagonalDownLeft.Remove(square);
                            // on supprime si la case contient une pièce de la même couleur que celle concernée par le déplacement
                        }
                        diagonalDownLeftEnd = true;
                    }
                }
                if (move.Piece.Square.X + i < 8 && move.Piece.Square.X + i >= 0 && move.Piece.Square.Y + i < 8 &&
                    move.Piece.Square.Y + i >= 0 && !diagonalDownRightEnd) {
                    Square square = board.Squares[move.Piece.Square.X + i, move.Piece.Square.Y + i];
                    diagonalDownRight.Add(square);
                    if (square.Piece != null) {
                        if (move.Piece.Color == square.Piece.Color) {
                            diagonalDownRight.Remove(square);
                            // on supprime si la case contient une pièce de la même couleur que celle concernée par le déplacement
                        }
                        diagonalDownRightEnd = true;
                    }
                }
            }

            List<Square> possibleSquares = diagonalUpLeft.Concat(diagonalUpRight)
                                                         .Concat(diagonalDownLeft)
                                                         .Concat(diagonalDownRight)
                                                         .ToList();

            if (possibleSquares.Contains(move.Square))
            {
                return true;
            }

            /**
             * +
             */

            Square destinationSquare = move.Square;
            Piece piece = move.Piece;
            //if the movement is not inline
            if (!(piece.Square.X == destinationSquare.X ^ piece.Square.Y == destinationSquare.Y)) return false;

            List<Square> line = new List<Square>();

            //Move along X
            if (piece.Square.Y == destinationSquare.Y) {
                line =
                    board.Squares.OfType<Square>()
                        .ToList()
                        .Where(x => Between(piece.Square.X, destinationSquare.X, x.X) && x.Y == destinationSquare.Y)
                        .ToList();
            } else {
                line =
                    board.Squares.OfType<Square>()
                        .ToList()
                        .Where(x => Between(piece.Square.Y, destinationSquare.Y, x.Y) && x.X == destinationSquare.X)
                        .ToList();
            }
            foreach (Square square1 in line) {
                Console.WriteLine(square1.Piece);
            }

            return line.All(betweenSquare => betweenSquare.Piece == null);

        }

        private bool Between(int i, int j, int x) {
            if (i > j)
                return i > x && j < x;
            else
                return j > x && i < x;
        }
    }
}