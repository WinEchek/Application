using System.Collections.Generic;
using WinEchek.Engine.Rules;
using WinEchek.Model;

namespace WinEchek.Engine.RuleManager
{
    public abstract class RuleGroup 
    {
        public RuleGroup Next { get; internal set; }
        protected List<IRule> Rules { get; set; } = new List<IRule>();

        public void AddGroup(RuleGroup ruleGroup)
        {
            if (Next == null)
                Next = ruleGroup;
            else
                Next.AddGroup(ruleGroup);
        }
        
        public abstract bool Handle(Move move);
    }
}