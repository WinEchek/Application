using System;
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
            WhitePlayer = new RealPlayer(Color.White);
            BlackPlayer = new RealPlayer(Color.Black);
            BoardView = new BoardView(Engine.Board, (RealPlayer) WhitePlayer);
            WhitePlayer.MoveDone += MoveHandler;
            _currentPlayer = WhitePlayer;
        }

        public Game(Engine.Engine engine, BoardView boardView)
        {
            Engine = engine;
            BoardView = boardView;
        }

        private void MoveHandler(Player sender, Move move)
        {
            if (sender != _currentPlayer) return; //Should tell the player it isn't his turn
            if (!Engine.DoMove(move)) _currentPlayer.Play();
            else _currentPlayer = _currentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
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