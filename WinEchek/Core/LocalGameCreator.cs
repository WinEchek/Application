namespace WinEchek.Core
{
    public class LocalGameCreator : GameCreator
    {
        public override Game CreateGame()
        {
            return new LocalGame();
        }
    }
}