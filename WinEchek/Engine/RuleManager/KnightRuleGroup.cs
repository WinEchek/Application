using WinEchek.Engine.Rules;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class KnightRuleGroup : RuleGroup
    {
        public KnightRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new KnightMovementRule());
        }
        protected override Type Type => Type.Knight;
    }
}