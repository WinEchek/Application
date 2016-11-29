using WinEchek.Engine;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public abstract class Player
    {
        public Color Color { get; internal set; }

        protected Player(Color color)
        {
            Color = color;
        }

        /// <summary>
        /// Notifie le joueur que c'est à son tour de jouer et que le Game peut recevoir un mouvement de sa part.
        /// Tant que ce mouvement n'est pas valide, cette méthode est appelée.
        /// </summary>
        public abstract void Play();

        public void Move(Move move)
        {
            MoveDone?.Invoke(this, move);
        }

        public delegate void MoveHandler(Player sender, Move move);
        public event MoveHandler MoveDone;
    }
}
