using System;
using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public class KingMovementRule : IRule
    {
        public bool IsMoveValid(Move move) =>
            Math.Abs(move.Piece.Square.X - move.TargetSquare.X) <= 1 &&
            Math.Abs(move.Piece.Square.Y - move.TargetSquare.Y) <= 1;
    }
}