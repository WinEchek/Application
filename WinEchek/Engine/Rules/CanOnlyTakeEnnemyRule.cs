using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class CanOnlyTakeEnnemyRule : IRule
    {
        public bool IsMoveValid(Move move) => 
            move.Piece.Color != move.TargetSquare?.Piece?.Color;

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList().FindAll(x => piece.Color != x?.Piece?.Color);
        }
    }
}