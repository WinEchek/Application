using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public interface IPlayer
    {
        void DoMove(Piece piece, Square square);
    }
}
