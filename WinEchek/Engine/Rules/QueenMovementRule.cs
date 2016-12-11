using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class QueenMovementRule : IRule
    {
        private BishopMovementRule _bishopRule = new BishopMovementRule();
        private RookMovementRule _rookRule = new RookMovementRule();

        public bool IsMoveValid(Move move) =>
            _bishopRule.IsMoveValid(move) || _rookRule.IsMoveValid(move);

        public List<Square> PossibleMoves(Piece piece)
        {
            return _bishopRule.PossibleMoves(piece)
                .Concat(_rookRule.PossibleMoves(piece))
                .ToList();
        }
    }
}