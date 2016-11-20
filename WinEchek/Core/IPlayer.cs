using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    //TODO the player should raise an event telling he's done a move || the game listening to the player 
    public interface IPlayer
    {
        void DoMove(Piece piece, Square square);
    }
}
