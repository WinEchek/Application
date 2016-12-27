using WinEchek.Engine.Rules;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.RuleManager
{
    public class QueenRuleGroup : RuleGroup
    {
        public QueenRuleGroup()
        {
            Rules.Add(new QueenMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Queen;
    }
}