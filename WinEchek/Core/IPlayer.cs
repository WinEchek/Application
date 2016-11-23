using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public interface IPlayer
    {
        bool DoMove(Move move);
    }
}
