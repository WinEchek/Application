using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using WinEchek.Model.Piece;
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

        public List<Square> PossibleMoves(Piece piece)
        {
            List<Square> result = new List<Square>();
            if (piece.Type == Type)
            {
                result = result.Concat(Rules.First().PossibleMoves(piece)).ToList();
                Rules.ForEach(x => result = result.Intersect(x.PossibleMoves(piece)).ToList());
                return result;
            }
            if (Next != null) return Next.PossibleMoves(piece);
            throw new Exception("NOBODY TREATS THIS PIECE !!! " + piece);
        }
    }
}