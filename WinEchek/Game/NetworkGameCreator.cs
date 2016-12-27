using System;
using WinEchek.Model;
using WinEchek.ModelView;

namespace WinEchek.Game
{
    public class NetworkGameCreator : GameCreator
    {
        public override Mode Mode => Mode.Network;

        public override Core.Game CreateGame(Container container, BoardView boardView)
        {
            throw new NotImplementedException();
        }
    }
}