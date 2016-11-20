using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public abstract class RuleGroup 
    {
        public RuleGroup Next { get; internal set; }

        public void Add(RuleGroup ruleGroup)
        {
            if (Next == null)
                Next = ruleGroup;
            else
                Next.Add(ruleGroup);
        }
        public abstract bool Handle(Piece piece, Square square);
    }
}