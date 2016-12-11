using System;
using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public class KnightMovementRule : IRule
    {
        public bool IsMoveValid(Move move) =>
            Math.Abs(move.TargetSquare.X - move.Piece.Square.X) == 2 && Math.Abs(move.TargetSquare.Y - move.Piece.Square.Y) == 1 ||
            Math.Abs(move.TargetSquare.Y - move.Piece.Square.Y) == 2 && Math.Abs(move.TargetSquare.X - move.Piece.Square.X) == 1;
    }
}