using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;
using WinEchek.Network;

namespace WinEchek.Game
{
    public class NetworkGameCreator : GameCreator
    {
        public override Mode Mode => Mode.Network;
        public override Core.Game CreateGame(Container container, BoardView boardView, Color color, GameCreatorParameters parameters)
        {
            IEngine engine = new RealEngine(container);
            Core.Game game;
            // Si le joueur local est blanc
            if (color == Color.White)
            {
                PlayerControler whitePlayerControler = new BoardViewPlayerController(boardView);
                PlayerControler blackPlayerControler = new NetworkPlayerController(NetworkServiceHost.GetNetworkServiceHost());

                Player whitePlayer = new Player(Color.White, whitePlayerControler);
                Player blackPlayer = new Player(Color.Black, blackPlayerControler);

                game = new Core.Game(engine, whitePlayer, blackPlayer, container, false);

                whitePlayer.Game = game;
                blackPlayer.Game = game;

                whitePlayerControler.Player = whitePlayer;
                blackPlayerControler.Player = blackPlayer;

                boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)whitePlayerControler);
            }
            else
            {
                PlayerControler whitePlayerControler = new NetworkPlayerController(NetworkServiceHost.GetNetworkServiceHost());
                PlayerControler blackPlayerControler = new BoardViewPlayerController(boardView);

                Player whitePlayer = new Player(Color.White, whitePlayerControler);
                Player blackPlayer = new Player(Color.Black, blackPlayerControler);

                game = new Core.Game(engine, whitePlayer, blackPlayer, container, false);

                whitePlayer.Game = game;
                blackPlayer.Game = game;

                whitePlayerControler.Player = whitePlayer;
                blackPlayerControler.Player = blackPlayer;

                boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)blackPlayerControler);
            }

            //SMTPLogger smtpLogger = new SMTPLogger(game);
            return game;

        }
    }
}