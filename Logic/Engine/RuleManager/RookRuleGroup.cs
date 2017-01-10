using WinEchek.Engine.Rules;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.RuleManager
{
    public class RookRuleGroup : RuleGroup
    {
        public RookRuleGroup()
        {
            Rules.Add(new RookMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Rook;
    }
}