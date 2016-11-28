using System;
using System.Collections.Generic;

namespace WinEchek.Core
{
    public class GameFactory
    {
        private Dictionary<Mode, GameCreator> _creators = new Dictionary<Mode, GameCreator>();

        /// <summary>
        /// Permet de créer une partie dans le mode de jeu souhaité
        /// </summary>
        /// <param name="mode">Mode de la partie à créer</param>
        /// <returns>Une partie de le mode souhaité</returns>
        /// <exception cref="Exception">Lève une exception que le mode de jeu passé en paramètre 
        /// n'as pas de créateur dans la fabrique avec comme message "Unknown game mode"</exception>
        public Game CreateGame(Mode mode)
        {
            foreach (KeyValuePair<Mode, GameCreator> creator in _creators)
            {
                if (creator.Key == mode)
                {
                    return creator.Value.CreateGame();
                }
            }
            throw new Exception("Unknown game mode");
        }
        /// <summary>
        /// Méthode pour ajouter un créateur de partie.
        /// </summary>
        /// <param name="mode">Mode de partie associé au créateur</param>
        /// <param name="gameCreator">Créateur de partie associé</param>
        public void AddCreator(Mode mode, GameCreator gameCreator) => _creators.Add(mode, gameCreator);
    }
}