using WinEchek.Engine.Rules;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class PawnRuleGroup : RuleGroup
    {
        public PawnRuleGroup()
        {
            Rules.Add(new PawnMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new WillNotMakeCheck());

        }

        protected override Type Type => Type.Pawn;
    }
}