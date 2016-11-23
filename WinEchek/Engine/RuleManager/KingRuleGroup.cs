using System;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class KingRuleGroup : RuleGroup
    {
        public KingRuleGroup()
        {
            Rules.Add(new KingMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
        }
        public override bool Handle(Move move)
        {
            //TODO could be refactored
            if (move.Piece.Type != Type.King)
            {
                if (Next != null)
                {
                    return Next.Handle(move);
                }
                throw new Exception("NOBODY TREATS THIS PIECE !!! " + move.Piece);
            }
            return Rules.All(rule => rule.IsMoveValid(move));
        }
    }
}