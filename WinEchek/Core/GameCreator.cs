using WinEchek.GUI;
using WinEchek.Model;

namespace WinEchek.Core
{
    public abstract class GameCreator
    {
        public abstract Mode Mode { get; }
        public abstract Game CreateGame(Board board, BoardView boardView);
    }

}