using WinEchek.GUI;
using WinEchek.Model;

namespace WinEchek.Core
{
    public class NetworkGameCreator : GameCreator
    {
        public override Mode Mode => Mode.Network;
        public override Game CreateGame(Container container, BoardView boardView)
        {
            throw new System.NotImplementedException();
        }
    }
}