using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core
{
    public class Player
    {
        public Color Color { get; internal set; }
        private PlayerControler _playerControler;
        private Game _game;

        protected Player(Color color, PlayerControler playerControler, Game game)
        {
            Color = color;
            _playerControler = playerControler;
            _game = game;
        }

        /// <summary>
        /// Notifie le joueur que c'est à son tour de jouer et que le Game peut recevoir un mouvement de sa part.
        /// Tant que ce mouvement n'est pas valide, cette méthode est appelée.
        /// </summary>
        public void Play() => _playerControler.Play();

        public List<Square> PossibleMoves(Piece piece) => _game.PossibleMoves(piece);

        public void Move(Move move)
        {
            MoveDone?.Invoke(this, move);
        }

        public delegate void MoveHandler(Player sender, Move move);
        public event MoveHandler MoveDone;
    }
}
