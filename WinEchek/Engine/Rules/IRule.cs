using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public interface IRule
    {
        /// <summary>
        /// Vérifie si un mouvement vérifie la règle
        /// </summary>
        /// <param name="move">Mouvement à vérifier par la règle</param>
        /// <returns>Vrai si le mouvement passé en paramètre est valide</returns>
        bool IsMoveValid(Move move);
        
        /// <summary>
        /// Récupère toutes les cases qui vérifie la règle pour la pièce donnée
        /// </summary>
        /// <param name="piece">Pièce qui effectue le mouvement</param>
        /// <returns>Liste des cases pour lesquelles la règle est vérifiée </returns>
        List<Square> PossibleMoves(Piece piece);
    }
}