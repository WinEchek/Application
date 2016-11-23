using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    //TODO new system idea the player raise an event telling he's done a move || the game listening to the player 
    public interface IPlayer
    {
        void DoMove(Move move);
    }
}
