using System;
using System.Diagnostics.Eventing.Reader;
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
        public Engine.Engine Engine { get; set; }
        public BoardView BoardView { get; set; }

        public Game()
        {
            Engine = new RealEngine(new Board());
            BoardView = new BoardView(Engine.Board, new RealPlayer(this, Color.White));
        }

        public Game(Engine.Engine engine, BoardView boardView)
        {
            Engine = engine;
            BoardView = boardView;
        }

        public void DoMove(Move move)
        {
            Square oldSquare = move.Piece.Square;
            if (!Engine.DoMove(move)) return;
        }

        public void Undo()
        {
            Engine.Undo();
        }

        public void Redo()
        {
            Engine.Redo();
        }

    }
}