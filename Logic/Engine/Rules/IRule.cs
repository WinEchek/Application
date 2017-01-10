using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.Rules
{
    public interface IRule
    {
        /// <summary>
        ///     Check if a move is correct against a rule
        /// </summary>
        /// <param name="move">Move to check</param>
        /// <param name="board">Board to apply the move on</param>
        /// <returns>False if the move is invalidated by this rule</returns>
        bool IsMoveValid(Move move, Board board);

        /// <summary>
        ///     Récupère toutes les cases qui vérifie la règle pour la pièce donnée
        /// </summary>
        /// <param name="piece">Pièce qui effectue le mouvement</param>
        /// <returns>Liste des cases pour lesquelles la règle est vérifiée </returns>
        List<Square> PossibleMoves(Piece piece);
    }
}