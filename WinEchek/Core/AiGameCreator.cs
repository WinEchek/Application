using WinEchek.Core.Persistance;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.IA;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public class AiGameCreator : GameCreator
    {
        public override Mode Mode => Mode.AI;
        public override Game CreateGame(Container container, BoardView boardView)
        {
            Engine.Engine engine = new RealEngine(container);
            PlayerControler whitePlayerControler = new BoardViewPlayerController(boardView);
            PlayerControler blackPlayerControler = new UciProcessController(container);
            Player whitePlayer = new Player(Color.White, whitePlayerControler);
            Player blackPlayer = new Player(Color.Black, blackPlayerControler);

            Game game = new Game(engine, whitePlayer, blackPlayer, container);

            whitePlayer.Game = game;
            blackPlayer.Game = game;

            whitePlayerControler.Player = whitePlayer;
            blackPlayerControler.Player = blackPlayer;

            boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)whitePlayerControler);

            //TODO Remvoe the logger
            SMTPLogger smtpLogger = new SMTPLogger(game);
            return game;
        }
    }
}