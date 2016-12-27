using WinEchek.Engine.Rules;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.RuleManager
{
    public class KnightRuleGroup : RuleGroup
    {
        public KnightRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new KnightMovementRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Knight;
    }
}