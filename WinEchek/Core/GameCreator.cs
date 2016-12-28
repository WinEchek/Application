using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public abstract class GameCreator
    {
        public abstract Mode Mode { get; }

        /// <summary>
        /// Demande au créateur de partie de renvoyer une partie fonctionnelle avec les paramètres souhaités
        /// </summary>
        /// <param name="container"></param>
        /// <param name="boardView"></param>
        /// <returns></returns>
        public abstract Game CreateGame(Container container, BoardView boardView);
    }

}