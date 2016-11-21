using System;
using System.Linq;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class PawnRuleGroup : RuleGroup
    {
        public PawnRuleGroup()
        {
            Rules.Add(new PawnMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
        }
        //TODO gérer les couleurs de pièces.
        public override bool Handle(Move move)
        {
            if (move.Piece.Type != Type.Pawn)
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