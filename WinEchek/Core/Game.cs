using System;
using System.Collections.Generic;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.Model;
using WinEchek.Model.Piece;


namespace WinEchek
{
    public class Game
    {
        private Player _currentPlayer;

        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }
        public Engine.Engine Engine { get; internal set; }
        public Container Container { get; set; }

        /// <summary>
        /// Construit une partie avec deux joueurs et un moteur
        /// </summary>
        /// <param name="engine">Moteur que la partie devra utilisée pour vérifier les coups</param>
        /// <param name="whitePlayer">Joueur blanc</param>
        /// <param name="blackPlayer">Joueur noir</param>
        public Game(Engine.Engine engine, Player whitePlayer, Player blackPlayer, Container container)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            Engine = engine;
            Container = container;

            WhitePlayer.MoveDone += MoveHandler;
            BlackPlayer.MoveDone += MoveHandler;

            _currentPlayer = WhitePlayer;
            _currentPlayer.Play();
        }

        /// <summary>
        /// Délégué appelé quand un joueur réalise un coup.
        /// </summary>
        /// <remarks>
        /// On vérifie si le coup est valide et si c'est le cas on demande à l'autre joueur de jouer.
        /// Sinon on indique au joueur que le coup est invalide afin qu'il nous redonne un coup.
        /// On réalise ces actions tant que la partie n'est pas echec et mat ou echec et pat.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="move"></param>
        private void MoveHandler(Player sender, Move move)
        {
            if (sender != _currentPlayer) return; //Tell the player it isn't his turn ?

            if (Engine.DoMove(move))
            {
                _currentPlayer.Stop();
                ChangePlayer();
                StateChanged?.Invoke(Engine.CurrentState());
            }

            _currentPlayer.Play();
        }
        
        private void ChangePlayer() => _currentPlayer = _currentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;

        #region Undo Redo

        /// <summary>
        /// Demande au moteur d'annuler le dernier coup joué
        /// </summary>
        public void Undo()
        {

            if (Engine.Undo())
            {
                _currentPlayer.Stop();
                ChangePlayer();
                StateChanged?.Invoke(Engine.CurrentState());
            }
            _currentPlayer.Play();
        }

        /// <summary>
        /// Demande au moteur de refaire le dernier coup annulé
        /// </summary>
        public void Redo()
        {
            if (Engine.Redo())
            {
                _currentPlayer.Stop();
                ChangePlayer();
                StateChanged?.Invoke(Engine.CurrentState());
            }
            _currentPlayer.Play();
        }

        #endregion

        /// <summary>
        /// Liste les mouvements possibles pour une pièce.
        /// </summary>
        /// <param name="piece">Pièce a tester</param>
        /// <returns>Liste des mouvements</returns>
        public List<Square> PossibleMoves(Piece piece) => Engine.PossibleMoves(piece);

        public delegate void StateHandler(BoardState state);
        public event StateHandler StateChanged;
    }
}