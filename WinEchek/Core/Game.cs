﻿using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    //TODO the game should implement a mode, two players and make them play together
    [Serializable]
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
            //TODO should be done in the local game
            BoardView = new BoardView(Engine.Board, true);
            WhitePlayer = new RealPlayer(Color.White, BoardView);
            BlackPlayer = new RealPlayer(Color.Black, BoardView);

            WhitePlayer.MoveDone += MoveHandler;
            BlackPlayer.MoveDone += MoveHandler;
            _currentPlayer = WhitePlayer;
            _currentPlayer.Play();
        }

        public Game(Engine.Engine engine, BoardView boardView)
        {
            Engine = engine;
            BoardView = boardView;
        }

        private void MoveHandler(Player sender, Move move)
        {
            if (sender != _currentPlayer) return; //Should tell the player it isn't his turn
            if (Engine.DoMove(move)) _currentPlayer = _currentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            _currentPlayer.Play();
            Console.WriteLine("Told " + _currentPlayer.Color + " player to play");
        }
        public bool DoMove(Move move) => Engine.DoMove(move);

        public void Undo()
        {
            Engine.Undo();
        }

        public void Redo()
        {
            Engine.Redo();
        }
    }


    public enum Mode
    {
        Local,
        Network,
        AI
    }
}