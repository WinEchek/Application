using WinEchek.Engine;
using WinEchek.GUI;

namespace WinEchek.Core
{
    public class LocalGame : Game
    {
        public LocalGame()
        {
        }

        public LocalGame(Engine.Engine engine, BoardView boardView) : base(engine, boardView)
        {
        }
    }
}