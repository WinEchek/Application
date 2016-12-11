using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class KnightMovementRule : IRule
    {
        public bool IsMoveValid(Move move) =>
            Math.Abs(move.TargetSquare.X - move.Piece.Square.X) == 2 && Math.Abs(move.TargetSquare.Y - move.Piece.Square.Y) == 1 ||
            Math.Abs(move.TargetSquare.Y - move.Piece.Square.Y) == 2 && Math.Abs(move.TargetSquare.X - move.Piece.Square.X) == 1;

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList().FindAll(x => IsMoveValid(new Move(piece, x)));
        }
    }
}