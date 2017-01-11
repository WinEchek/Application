using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.IA;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;

namespace WinEchek.Game
{
    public class AiGameCreator : GameCreator
    {
        public override Mode Mode => Mode.AI;

        public override Core.Game CreateGame(Container container, BoardView boardView, Color color, GameCreatorParameters parameters)
        {
            IEngine engine = new RealEngine(container);
            PlayerControler whitePlayerControler = new BoardViewPlayerController(boardView);
            PlayerControler blackPlayerControler = new UciProcessController(container, parameters.AiSearchType, parameters.AiSkillLevel, parameters.AiSearchValue);
            Player whitePlayer = new Player(Color.White, whitePlayerControler);
            Player blackPlayer = new Player(Color.Black, blackPlayerControler);

            Core.Game game = new Core.Game(engine, whitePlayer, blackPlayer, container, true);

            whitePlayer.Game = game;
            blackPlayer.Game = game;

            whitePlayerControler.Player = whitePlayer;
            blackPlayerControler.Player = blackPlayer;

            boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController) whitePlayerControler);

            //TODO Remvoe the logger
            //SMTPLogger smtpLogger = new SMTPLogger(game);
            return game;
        }
    }
}