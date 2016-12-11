using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class KingMovementRule : IRule
    {
        public bool IsMoveValid(Move move) =>
            Math.Abs(move.Piece.Square.X - move.Square.X) <= 1 &&
            Math.Abs(move.Piece.Square.Y - move.Square.Y) <= 1;

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x)));  
        }
    }
}