using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.Rules
{
    internal class BishopMovementRule : IRule
    {
        //TODO Refactor (copy/paste)
        /// <inheritdoc />
        public bool IsMoveValid(Move move, Board board)
            => PossibleMoves(board.PieceAt(move.StartCoordinate)).Contains(board.SquareAt(move.TargetCoordinate));

        /// <inheritdoc />
        public List<Square> PossibleMoves(Piece piece)
        {
            List<Square> diagonalUpLeft = new List<Square>();
            List<Square> diagonalUpRight = new List<Square>();
            List<Square> diagonalDownLeft = new List<Square>();
            List<Square> diagonalDownRight = new List<Square>();

            bool diagonalUpLeftEnd = false;
            bool diagonalUpRightEnd = false;
            bool diagonalDownLeftEnd = false;
            bool diagonalDownRightEnd = false;


            Board board = piece.Square.Board;

            for (int i = 1; i < 8; i++)
            {
                if ((piece.Square.X - i < 8) && (piece.Square.X - i >= 0) && (piece.Square.Y - i < 8) &&
                    (piece.Square.Y - i >= 0) && !diagonalUpLeftEnd)
                {
                    Square square = board.Squares[piece.Square.X - i, piece.Square.Y - i];
                    diagonalUpLeft.Add(square);
                    if (square.Piece != null)
                    {
                        if (piece.Color == square.Piece.Color) diagonalUpLeft.Remove(square);
                        diagonalUpLeftEnd = true;
                    }
                }
                if ((piece.Square.X + i < 8) && (piece.Square.X + i >= 0) && (piece.Square.Y - i < 8) &&
                    (piece.Square.Y - i >= 0) && !diagonalUpRightEnd)
                {
                    Square square = board.Squares[piece.Square.X + i, piece.Square.Y - i];
                    diagonalUpRight.Add(square);
                    if (square.Piece != null)
                    {
                        if (piece.Color == square.Piece.Color) diagonalUpRight.Remove(square);
                        diagonalUpRightEnd = true;
                    }
                }
                if ((piece.Square.X - i < 8) && (piece.Square.X - i >= 0) && (piece.Square.Y + i < 8) &&
                    (piece.Square.Y + i >= 0) && !diagonalDownLeftEnd)
                {
                    Square square = board.Squares[piece.Square.X - i, piece.Square.Y + i];
                    diagonalDownLeft.Add(square);
                    if (square.Piece != null)
                    {
                        if (piece.Color == square.Piece.Color) diagonalDownLeft.Remove(square);
                        diagonalDownLeftEnd = true;
                    }
                }
                if ((piece.Square.X + i < 8) && (piece.Square.X + i >= 0) && (piece.Square.Y + i < 8) &&
                    (piece.Square.Y + i >= 0) && !diagonalDownRightEnd)
                {
                    Square square = board.Squares[piece.Square.X + i, piece.Square.Y + i];
                    diagonalDownRight.Add(square);
                    if (square.Piece != null)
                    {
                        if (piece.Color == square.Piece.Color) diagonalDownRight.Remove(square);
                        diagonalDownRightEnd = true;
                    }
                }
            }

            List<Square> possibleSquares = diagonalUpLeft.Concat(diagonalUpRight)
                .Concat(diagonalDownLeft)
                .Concat(diagonalDownRight)
                .ToList();
            return possibleSquares;
        }
    }
}