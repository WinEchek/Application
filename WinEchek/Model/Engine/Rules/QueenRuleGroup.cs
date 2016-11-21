using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class QueenRuleGroup : RuleGroup
    {
        public override bool Handle(Move move)
        {
            if (move.Piece.Type != Type.Queen)
            {
                if (Next != null)
                {
                    return Next.Handle(move);
                }
                throw new Exception("NOBODY TREATS THIS PIECE !!! " + move.Piece);
            }
            return true;
        }
    }
}