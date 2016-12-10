using System;
using System.Collections.Generic;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.GUI;
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
        public BoardView BoardView { get; set; }

        public Game()
        {
            Engine = new RealEngine(new Board());

            BoardView = new BoardView(Engine.Board, true);


            WhitePlayer.MoveDone += MoveHandler;
            BlackPlayer.MoveDone += MoveHandler;

            _currentPlayer = WhitePlayer;
            _currentPlayer.Play();
        }

        private void MoveHandler(Player sender, Move move)
        {
            if (sender != _currentPlayer) return; //Tell the player it isn't his turn ?

            if (Engine.DoMove(move)) _currentPlayer = _currentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            _currentPlayer.Play();
        }

        public void Undo() => Engine.Undo();

        public void Redo() => Engine.Redo();

        public List<Square> PossibleMoves(Piece piece)
        {
            throw new NotImplementedException();
        }
    }

    public enum Mode
    {
        Local,
        Network,
        AI
    }
}