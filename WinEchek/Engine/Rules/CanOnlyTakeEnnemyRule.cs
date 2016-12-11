using WinEchek.Model;

namespace WinEchek.Engine.Rules
{
    public class CanOnlyTakeEnnemyRule : IRule
    {
        public bool IsMoveValid(Move move) => 
            move.Piece.Color != move.TargetSquare?.Piece?.Color;
    }
}