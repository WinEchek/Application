using System;
using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public class KingMovementRule : IRule
    {
        public bool IsMoveValid(Move move) =>
            Math.Abs(move.Piece.Square.X - move.Square.X) <= 1 &&
            Math.Abs(move.Piece.Square.Y - move.Square.Y) <= 1;
    }
}