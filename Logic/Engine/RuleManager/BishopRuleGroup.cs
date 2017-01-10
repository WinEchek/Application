using WinEchek.Engine.Rules;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.RuleManager
{
    public class BishopRuleGroup : RuleGroup
    {
        public BishopRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new BishopMovementRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Bishop;
    }
}