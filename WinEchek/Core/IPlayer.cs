using System;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public interface IPlayer
    {
        /// <summary>
        /// Notifie le joueur que c'est à son tour de jouer et que le Game peut recevoir un mouvement de sa part.
        /// Tant que ce mouvement n'est pas valide, cette méthode est appelée.
        /// </summary>
        Move Play();
    }
}
