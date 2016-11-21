using System.Runtime.CompilerServices;
using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    internal class BishopMovementRule : IRule
    {
        public bool IsMoveValid(Move move)
        {
            throw new System.NotImplementedException();
        }

        private bool isInDiagonal(int x, int y, int X, int Y)
        {
            return true;
        }
    }
}