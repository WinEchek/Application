using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.States
{
    public interface IState
    {
        /// <summary>
        ///     Vérifie si un plateau est oui ou non dans l'état défini par la classe
        /// </summary>
        /// <param name="board">Plateau à vérifier</param>
        /// <param name="color"></param>
        /// <returns>Oui si le plateau est dans l'état défini par la classe</returns>
        bool IsInState(Board board, Color color);

        /// <summary>
        ///     Explique l'état en une courte phrase
        /// </summary>
        /// <returns></returns>
        string Explain();
    }
}