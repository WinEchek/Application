namespace WinEchek.Core
{
    public class WinEchek
    {
        public Game Game { get; set; }

        public WinEchek()
        {

        }

        public void CreateGame()
        {
            Game = new Game();
        }
    }
}