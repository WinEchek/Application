using System;
using WinEchek.GUI.Core.FlyoutContent;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class PawnRuleGroup : RuleGroup
    {
        public PawnRuleGroup()
        {
            Rules.Add(new PawnMovementRule());
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
            bool result = true;
            foreach (IRule rule in Rules)
            {
                result = rule.IsMoveValid(move); //TODO WIP
            }
            return result;
        }
    }
}