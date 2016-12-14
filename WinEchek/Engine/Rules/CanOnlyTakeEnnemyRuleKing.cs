using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    internal class CanOnlyTakeEnnemyRuleKing : IRule
    {
        public bool IsMoveValid(Move move)
        {
            if (move.Piece.Color == move.TargetSquare?.Piece?.Color)
            {
                Console.WriteLine("Can only roi");
                return move.TargetSquare.Piece.Type == Type.Rook;
            }
                
            return true;
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x)));
        }
    }
}