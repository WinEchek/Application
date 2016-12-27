using WinEchek.Model;
using WinEchek.ModelView;

namespace WinEchek.Game
{
    public abstract class GameCreator
    {
        public abstract Mode Mode { get; }
        public abstract Core.Game CreateGame(Container container, BoardView boardView);
    }
}