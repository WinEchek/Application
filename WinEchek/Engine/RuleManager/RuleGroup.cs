using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public abstract class RuleGroup 
    {
        public RuleGroup Next { get; internal set; }
        protected List<IRule> Rules { get; set; } = new List<IRule>();
        protected abstract Type Type { get; }

        public void AddGroup(RuleGroup ruleGroup)
        {
            if (Next == null)
                Next = ruleGroup;
            else
                Next.AddGroup(ruleGroup);
        }
        
        public bool Handle(Move move)
        {
            if (move.Piece.Type == Type) return Rules.All(rule => rule.IsMoveValid(move));
            if (Next != null) return Next.Handle(move);
            throw new Exception("NOBODY TREATS THIS PIECE !!! " + move.Piece);
        }
    }
}