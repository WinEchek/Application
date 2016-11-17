using System;
using System.Diagnostics.Eventing.Reader;
using WinEchek.Command;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
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

        public void DoMove(Piece piece, Square square)
        {
            Square oldSquare = piece.Square;
            if (!Engine.DoMove(piece, square)) return;
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