using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;
using WinEchek.Network;

namespace WinEchek.Game
{
    public class GameFactory
    {
        public List<GameCreator> GameCreators = new List<GameCreator>();

        public GameFactory()
        {
            GameCreators.Add(new LocalGameCreator());
            GameCreators.Add(new AiGameCreator());
            GameCreators.Add(new NetworkGameCreator());
        }

        /// <summary>
        /// Retourne une instance de partie dans le mode de jeu passé en paramètre
        /// </summary>
        /// <param name="mode">Mode de jeu souhaité</param>
        /// <returns>Une partie dans le mode de jeu passé en paramètre</returns>
        public Core.Game CreateGame(Mode mode, Container container, BoardView boardView, Color color)
        {
            return GameCreators.FindAll(x => x.Mode == mode).First().CreateGame(container, boardView, color);
        }
    }


    /// <summary>
    /// Défini les différents mode de jeu possibles
    /// </summary>
    public enum Mode
    {
        Local,
        Network,
        AI
    }
}