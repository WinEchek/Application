using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public interface IRule
    {
        bool IsMoveValid(Move move);
    }
}