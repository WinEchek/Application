using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public abstract class PieceRule 
    {
        public PieceRule Next { get; internal set; }

        public void Add(PieceRule pieceRule)
        {
            if (Next == null)
                Next = pieceRule;
            else
                Next.Add(pieceRule);
        }
        public abstract bool Handle(Piece piece, Square square);
    }
}