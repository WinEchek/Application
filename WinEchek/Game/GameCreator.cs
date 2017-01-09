using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;

namespace WinEchek.Game
{
    public abstract class GameCreator
    {
        public abstract Mode Mode { get; }

        /// <summary>
        /// Demande au créateur de partie de renvoyer une partie fonctionnelle avec les paramètres souhaités
        /// </summary>
        /// <param name="container"></param>
        /// <param name="boardView"></param>
        /// <param name="color">La couleur</param>
        /// <returns></returns>
        public abstract Core.Game CreateGame(Container container, BoardView boardView, Color color, GameCreatorParameters parameters);
    }

}