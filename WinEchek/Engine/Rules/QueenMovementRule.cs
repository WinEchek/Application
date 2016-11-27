using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public class QueenMovementRule : IRule
    {
        private BishopMovementRule _bishopRule = new BishopMovementRule();
        private RookMovementRule _rookRule = new RookMovementRule();

        public bool IsMoveValid(Move move) =>
            _bishopRule.IsMoveValid(move) || _rookRule.IsMoveValid(move);
    }
}